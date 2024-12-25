using Contract.DTOs.UserDTO;
using Contract.Event.RecipeEvent;
using MassTransit;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Recipes;

public class GetRecipeFeedsCommand : IRequest<Result<PaginatedRecipeFeedsListResponse>>
{
    [Required]
    public int Skip {  get; init; }

    [Required]
    public List<string>? TagValues { get; init; }
}

public class GetRecipeFeedsCommandHandler : IRequestHandler<GetRecipeFeedsCommand, Result<PaginatedRecipeFeedsListResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IBus _bus;
    private readonly IPaginateDataUtility<Recipe, CommonPaginatedMetadata> _paginateDataUtility;

    public GetRecipeFeedsCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IPaginateDataUtility<Recipe, CommonPaginatedMetadata> paginateDataUtility, IBus bus)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _bus = bus;
    }

    public  async Task<Result<PaginatedRecipeFeedsListResponse>> Handle(GetRecipeFeedsCommand request, CancellationToken cancellationToken)
    {
        var tagValues = request.TagValues;
        var skip = request.Skip;

        var recipesQuery = _context.Recipes.OrderByDescending(r => r.CreatedAt).AsQueryable();

        if(tagValues != null && tagValues.Any())
        {
            recipesQuery = recipesQuery.Where(r => tagValues.Any(tag =>
                r.Title.ToLower().Contains(tag.ToLower()) ||
                r.Description.ToLower().Contains(tag.ToLower()) ||
                r.Ingredients.Any(ingredient => ingredient.ToLower().Contains(tag.ToLower()))
            ));
        }

        var totalPage = (await recipesQuery.CountAsync() + RECIPE_CONSTANTS.RECIPE_LIMIT - 1) / RECIPE_CONSTANTS.RECIPE_LIMIT;

        recipesQuery = _paginateDataUtility.PaginateQuery(recipesQuery, new PaginateParam
        {
            Offset = skip * RECIPE_CONSTANTS.RECIPE_LIMIT,
            Limit = RECIPE_CONSTANTS.RECIPE_LIMIT
        });

        var recipeList = await recipesQuery.Select(r =>
        new RecipeFeedResponse
        {
            AuthorAvtUrl = "",
            AuthorDisplayName = "",
            AuthorId = r.AuthorId,
            Description = r.Description,
            Id = r.Id,
            Title = r.Title,
            NumberOfComment = r.NumberOfComment,
            VoteDiff = r.VoteDiff,
        }).ToListAsync();

        if (recipeList == null || !recipeList.Any())
        {
            return Result<PaginatedRecipeFeedsListResponse>.Success(new PaginatedRecipeFeedsListResponse
            {
                Metadata = new CommonPaginatedMetadata
                {
                    TotalPage = 0
                },
                PaginatedData = []
            });
        }

        var authorIds = recipesQuery
        .Select(r => r.AuthorId)
        .Distinct()
        .ToHashSet();

        var requestClient = _bus.CreateRequestClient<GetRecipesEvent>();

        var response = await requestClient.GetResponse<GetUsersForDisplayRecipeDTO>(new GetRecipesEvent
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

        var paginatedResponse = new PaginatedRecipeFeedsListResponse
        {
            PaginatedData = recipeList,
            Metadata = new CommonPaginatedMetadata
            {
                TotalPage = totalPage,
            }
        };
        return Result<PaginatedRecipeFeedsListResponse>.Success(paginatedResponse);
    }
}
