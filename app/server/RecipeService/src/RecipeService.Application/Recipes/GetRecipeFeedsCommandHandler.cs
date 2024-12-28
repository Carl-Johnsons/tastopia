using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Recipes;

public class GetRecipeFeedsCommand : IRequest<Result<PaginatedRecipeFeedsListResponse?>>
{
    [Required]
    public int? Skip {  get; init; }

    [Required]
    public List<string>? TagValues { get; init; }
}

public class GetTagsCommandHandler : IRequestHandler<GetRecipeFeedsCommand, Result<PaginatedRecipeFeedsListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IServiceBus _serviceBus;
    private readonly IPaginateDataUtility<Recipe, AdvancePaginatedMetadata> _paginateDataUtility;

    public GetTagsCommandHandler(IApplicationDbContext context, IServiceBus serviceBus, IPaginateDataUtility<Recipe, AdvancePaginatedMetadata> paginateDataUtility)
    {
        _context = context;
        _serviceBus = serviceBus;
        _paginateDataUtility = paginateDataUtility;
    }

    public  async Task<Result<PaginatedRecipeFeedsListResponse?>> Handle(GetRecipeFeedsCommand request, CancellationToken cancellationToken)
    {
        var tagValues = request.TagValues;
        var skip = request.Skip;

        if (skip == null || tagValues == null || !tagValues.Any())
        {
            return Result<PaginatedRecipeFeedsListResponse>.Failure(RecipeError.NotFound);
        }

        tagValues.RemoveAll(string.IsNullOrEmpty);

        if (!tagValues.Any())
        {
            return Result<PaginatedRecipeFeedsListResponse>.Failure(RecipeError.NotFound);
        }


        var recipesQuery = _context.Recipes.OrderByDescending(r => r.CreatedAt).AsQueryable();


        if (!tagValues.Contains("All"))
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
            Offset = (skip ?? 0) * RECIPE_CONSTANTS.RECIPE_LIMIT,
            Limit = RECIPE_CONSTANTS.RECIPE_LIMIT
        });

        var recipeList = await recipesQuery.Select(r =>
        new RecipeFeedResponse
        {
            AuthorAvtUrl = "",
            AuthorDisplayName = "",
            AuthorId = r.AuthorId,
            RecipeImgUrl = r.ImageUrl,
            Description = r.Description,
            Id = r.Id,
            Title = r.Title,
            NumberOfComment = r.NumberOfComment,
            VoteDiff = r.VoteDiff,
        }).ToListAsync();

        if (recipeList == null || !recipeList.Any())
        {
            return Result<PaginatedRecipeFeedsListResponse?>.Success(new PaginatedRecipeFeedsListResponse
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

        var requestClient = _serviceBus.CreateRequestClient<GetSimpleUsersEvent>();

        var response = await requestClient.GetResponse<GetSimpleUsersDTO>(new GetSimpleUsersEvent
        {
            AccountIds = authorIds,
        });

        if (response == null || response.Message.Users.Count != authorIds.Count)
        {
            return Result<PaginatedRecipeFeedsListResponse>.Failure(RecipeError.NotFound);
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

        var paginatedResponse = new PaginatedRecipeFeedsListResponse
        {
            PaginatedData = recipeList,
            Metadata = new AdvancePaginatedMetadata
            {
                TotalPage = totalPage,
                HasNextPage = hasNextPage,
            }
        };
        return Result<PaginatedRecipeFeedsListResponse?>.Success(paginatedResponse);
    }
}
