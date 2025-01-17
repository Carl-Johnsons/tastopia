using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using MassTransit.Initializers;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;
using UserProto;

namespace RecipeService.Application.UserBookmarkRecipes.Queries;

public class GetRecipeBookmarksQuery : IRequest<Result<PaginatedRecipeFeedsListResponse?>>
{
    [Required]
    public int? Skip { get; init; }

    [Required]
    public Guid? AccountId { get; init; }

}

public class GetRecipeBookmarksQueryHandler : IRequestHandler<GetRecipeBookmarksQuery, Result<PaginatedRecipeFeedsListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<Recipe, AdvancePaginatedMetadata> _paginateDataUtility;
    private readonly IMapper _mapper;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public GetRecipeBookmarksQueryHandler(IApplicationDbContext context, IPaginateDataUtility<Recipe, AdvancePaginatedMetadata> paginateDataUtility, IMapper mapper, GrpcUser.GrpcUserClient grpcUserClient)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _mapper = mapper;
        _grpcUserClient = grpcUserClient;
    }

    public async Task<Result<PaginatedRecipeFeedsListResponse?>> Handle(GetRecipeBookmarksQuery request, CancellationToken cancellationToken)
    {
        var skip = request.Skip;
        var accountId = request.AccountId;

        if (skip == null || accountId == null)
        {
            return Result<PaginatedRecipeFeedsListResponse>.Failure(RecipeError.NotFound);
        }

        var bookmarkQuery = _context.GetDatabase().GetCollection<UserBookmarkRecipe>(nameof(UserBookmarkRecipe)).AsQueryable()
                         .Where(bm => bm.AccountId == accountId).OrderByDescending(c => c.CreatedAt).AsQueryable();

        var recipeQuery = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable().AsQueryable();

        var resultQuery = bookmarkQuery.Join(recipeQuery,
            bm => bm.RecipeId,
            r => r.Id,
            (bm, r) => r
        );

        var totalPage = (resultQuery.Count() + RECIPE_CONSTANTS.RECIPE_LIMIT - 1) / RECIPE_CONSTANTS.RECIPE_LIMIT;

        resultQuery = _paginateDataUtility.PaginateQuery(resultQuery!, new PaginateParam
        {
            Offset = (skip ?? 0) * RECIPE_CONSTANTS.RECIPE_LIMIT,
            Limit = RECIPE_CONSTANTS.RECIPE_LIMIT
        });

        var recipeList = resultQuery.Select(r =>
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
            Vote = Vote.None.ToString(),

        }).ToList();
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

        var recipeMap = resultQuery.ToDictionary(r => r!.Id);
        var voteDict = new Dictionary<Tuple<Guid, Guid>, Vote>();
        foreach (var (key, value) in recipeMap)
        {
            var vote = recipeMap[key]!.RecipeVotes.Where(v => v.AccountId == accountId).SingleOrDefault();
            voteDict.Add(Tuple.Create(key, value!.AuthorId), vote != null ? vote.IsUpvote ? Vote.Upvote : Vote.Downvote : Vote.None);
        }

        foreach (var recipe in recipeList)
        {
            recipe.Vote = voteDict[Tuple.Create(recipe.Id, recipe.AuthorId)].ToString();
        }

        var authorIds = resultQuery
        .Select(r => r.AuthorId)
        .Distinct()
        .ToHashSet();

        var response = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { _mapper.Map<RepeatedField<string>>(authorIds) }
        }, cancellationToken: cancellationToken);

        var mapUsers = new Dictionary<Guid, SimpleUser>();
        foreach (var (key, value) in response.Users)
        {
            mapUsers[Guid.Parse(key)] = new SimpleUser
            {
                AccountId = Guid.Parse(value.AccountId),
                AvtUrl = value.AvtUrl,
                DisplayName = value.DisplayName,
            };
        }

        if (response == null || mapUsers.Count != authorIds.Count)
        {
            return Result<PaginatedRecipeFeedsListResponse>.Failure(RecipeError.NotFound);
        }


        foreach (var recipe in recipeList)
        {
            recipe.AuthorDisplayName = mapUsers[recipe.AuthorId].DisplayName;
            recipe.AuthorAvtUrl = mapUsers[recipe.AuthorId].AvtUrl;
        }

        var hasNextPage = true;

        if (skip >= totalPage - 1)
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
