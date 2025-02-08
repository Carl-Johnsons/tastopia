using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;
using UserProto;

namespace RecipeService.Application.Recipes.Queries;

public class GetRecipeFeedsQuery : IRequest<Result<PaginatedRecipeFeedsListResponse?>>
{
    [Required]
    public int? Skip { get; init; }

    [Required]
    public List<string>? TagValues { get; init; }

    [Required]
    public Guid? AccountId { get; init; }
}

public class GetRecipeFeedsQueryHandler : IRequestHandler<GetRecipeFeedsQuery, Result<PaginatedRecipeFeedsListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<Recipe, AdvancePaginatedMetadata> _paginateDataUtility;
    private readonly IMapper _mapper;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public GetRecipeFeedsQueryHandler(IApplicationDbContext context, IPaginateDataUtility<Recipe, AdvancePaginatedMetadata> paginateDataUtility, IMapper mapper, GrpcUser.GrpcUserClient grpcUserClient)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _mapper = mapper;
        _grpcUserClient = grpcUserClient;
    }

    public async Task<Result<PaginatedRecipeFeedsListResponse?>> Handle(GetRecipeFeedsQuery request, CancellationToken cancellationToken)
    {
        var tagValues = request.TagValues;
        var skip = request.Skip;
        var accountId = request.AccountId;

        if (skip == null || tagValues == null || !tagValues.Any() || accountId == null)
        {
            return Result<PaginatedRecipeFeedsListResponse>.Failure(RecipeError.NotFound);
        }

        tagValues.RemoveAll(string.IsNullOrEmpty);

        if (!tagValues.Any())
        {
            return Result<PaginatedRecipeFeedsListResponse>.Failure(RecipeError.NotFound);
        }


        var recipesQuery = _context.Recipes.Where(r => r.IsActive == true).OrderByDescending(r => r.CreatedAt).AsQueryable();


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
            Vote = Vote.None.ToString(),

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


        var recipeMap = await recipesQuery.ToDictionaryAsync(r => r.Id);
        var voteDict = new Dictionary<Tuple<Guid, Guid>, Vote>();
        foreach (var (key, value) in recipeMap)
        {
            var vote = recipeMap[key].RecipeVotes.Where(v => v.AccountId == accountId).SingleOrDefault();
            voteDict.Add(Tuple.Create(key, value.AuthorId), vote != null ? (vote.IsUpvote ? Vote.Upvote : Vote.Downvote) : Vote.None);
        }

        foreach (var recipe in recipeList)
        {
            recipe.Vote = voteDict[Tuple.Create(recipe.Id, recipe.AuthorId)].ToString();
        }

        var authorIds = recipesQuery
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
