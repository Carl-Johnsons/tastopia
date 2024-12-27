using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using MassTransit;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Recipes;

public class SearchRecipesCommand : IRequest<Result<PaginatedSearchRecipeListResponse?>>
{
    [Required]
    public int? Skip {  get; init; }

    [Required]
    public List<string>? TagValues { get; init; }

    [Required]
    public string? Keyword { get; init; }

    [Required]
    public Guid UserId { get; init; }
}

public class SearchRecipesCommandHandler : IRequestHandler<SearchRecipesCommand, Result<PaginatedSearchRecipeListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IBus _bus;
    private readonly IPaginateDataUtility<Recipe, AdvancePaginatedMetadata> _paginateDataUtility;

    public SearchRecipesCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IPaginateDataUtility<Recipe, AdvancePaginatedMetadata> paginateDataUtility, IBus bus)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _bus = bus;
    }

    public  async Task<Result<PaginatedSearchRecipeListResponse?>> Handle(SearchRecipesCommand request, CancellationToken cancellationToken)
    {
        var skip = request.Skip;
        var tagValues = request.TagValues;
        var keyword = request.Keyword;

        if(skip == null) {
            return Result<PaginatedSearchRecipeListResponse?>.Failure(RecipeError.NotFound);
        }

        var recipesQuery = _context.Recipes.OrderByDescending(r => r.CreatedAt).AsQueryable();

        if (tagValues != null && tagValues.Any())
        {
            recipesQuery = recipesQuery.Where(r => tagValues.Any(tag =>
                r.Title.ToLower().Contains(tag.ToLower()) ||
                r.Description.ToLower().Contains(tag.ToLower()) ||
                r.Ingredients.Any(ingredient => ingredient.ToLower().Contains(tag.ToLower()))
            ));
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            recipesQuery = recipesQuery.Where(r =>
                r.Title.ToLower().Contains(keyword.ToLower()) ||
                r.Description.ToLower().Contains(keyword.ToLower()) ||
                r.Ingredients.Any(ingredient => ingredient.ToLower().Contains(keyword.ToLower()))
            );
        }

        var result = await recipesQuery.ToListAsync();


        var totalPage = (await recipesQuery.CountAsync() + RECIPE_CONSTANTS.RECIPE_LIMIT - 1) / RECIPE_CONSTANTS.RECIPE_LIMIT;


        recipesQuery = _paginateDataUtility.PaginateQuery(recipesQuery, new PaginateParam
        {
            Offset = skip ?? 0 * RECIPE_CONSTANTS.RECIPE_LIMIT,
            Limit = RECIPE_CONSTANTS.RECIPE_LIMIT
        });

        var recipeList = await recipesQuery.Select(r =>
        new SearchRecipesResponse
        {
            AuthorAvtUrl = "",
            AuthorDisplayName = "",
            AuthorId = r.AuthorId,
            Description = r.Description,
            Id = r.Id,
            Title = r.Title,
        }).ToListAsync();

        if (recipeList == null || !recipeList.Any())
        {
            return Result<PaginatedSearchRecipeListResponse?>.Success(new PaginatedSearchRecipeListResponse
            {
                PaginatedData = [],
                Metadata = new AdvancePaginatedMetadata
                {
                    HasNextPage = false,
                    TotalPage = 0,
                }
            });
        }

        var authorIds = recipesQuery
        .Select(r => r.AuthorId)
        .Distinct()
        .ToHashSet();

        var requestClient = _bus.CreateRequestClient<GetSimpleUsersEvent>();

        var response = await requestClient.GetResponse<GetSimpleUsersDTO>(new GetSimpleUsersEvent
        {
            UserIds = authorIds,
        });

        if (response == null || response.Message.Users.Count != authorIds.Count)
        {
            throw new Exception("Invalid response");
        }

        var mapUser = response.Message.Users;

        foreach(var recipe in recipeList)
        {
            recipe.AuthorDisplayName = mapUser[recipe.AuthorId].DisplayName;
            recipe.AuthorAvtUrl = mapUser[recipe.AuthorId].AvtUrl;
        }

        var hasNextPage = true;

        if(skip >= totalPage - 1)
        {
            hasNextPage = false;
        }

        var paginatedResponse = new PaginatedSearchRecipeListResponse
        {
            PaginatedData = recipeList,
            Metadata = new AdvancePaginatedMetadata
            {
                TotalPage = totalPage,
                HasNextPage = hasNextPage
            }
        };
        return Result<PaginatedSearchRecipeListResponse?>.Success(paginatedResponse);
    }
}
