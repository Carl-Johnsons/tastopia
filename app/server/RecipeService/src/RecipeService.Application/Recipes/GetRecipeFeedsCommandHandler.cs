using Contract.DTOs.UserDTO;
using Contract.Event.RecipeEvent;
using MassTransit;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Recipes;

public class GetRecipeFeedsCommand : IRequest<Result<PaginatedRecipeListResponse>>
{
    [Required]
    public int Skip {  get; init; }

    [Required]
    public List<string>? TagValues { get; init; }
}

public class GetRecipeFeedsCommandHandler : IRequestHandler<GetRecipeFeedsCommand, Result<PaginatedRecipeListResponse>>
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

    public  async Task<Result<PaginatedRecipeListResponse>> Handle(GetRecipeFeedsCommand request, CancellationToken cancellationToken)
    {
        var tagValues = request.TagValues;
        var skip = request.Skip;

        var recipesQuery = _context.Recipes.AsQueryable();

        if(tagValues != null && tagValues.Any())
        {
            recipesQuery = _context.Recipes.Where(r => tagValues.Any(tag =>
                r.Title.Contains(tag) ||
                r.Description.Contains(tag) ||
                r.Ingredients.Any(ingredient => ingredient.Contains(tag))
            )).AsQueryable();
        }

        var totalPage = (await recipesQuery.CountAsync() + RECIPE_CONSTANTS.RECIPE_LIMIT - 1) / RECIPE_CONSTANTS.RECIPE_LIMIT;

        recipesQuery = _paginateDataUtility.PaginateQuery(recipesQuery, new PaginateParam
        {
            Offset = request.Skip * RECIPE_CONSTANTS.RECIPE_LIMIT,
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
            NumberOfComment = 0,
            VoteDiff = r.VoteDiff ?? 0,
        }).ToListAsync();

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

        var paginatedResponse = new PaginatedRecipeListResponse
        {
            PaginatedData = recipeList,
            Metadata = new CommonPaginatedMetadata
            {
                TotalPage = totalPage,
            }
        };
        return Result<PaginatedRecipeListResponse>.Success(paginatedResponse);
    }
}
