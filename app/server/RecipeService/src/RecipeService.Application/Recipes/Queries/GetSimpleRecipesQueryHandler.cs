using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using static UserProto.GrpcUser;
using UserProto;
using MongoDB.Driver;

namespace RecipeService.Application.Recipes.Queries;

public class GetSimpleRecipesQuery : IRequest<Result<List<SimpleRecipeResponse>?>>
{
    public Guid AccountId { get; set; }
    public HashSet<Guid> RecipeIds { get; set; } = null!;
}

public class GetSimpleRecipesQueryHandler : IRequestHandler<GetSimpleRecipesQuery, Result<List<SimpleRecipeResponse>?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public GetSimpleRecipesQueryHandler(IApplicationDbContext context, IMapper mapper, GrpcUserClient grpcUserClient)
    {
        _context = context;
        _mapper = mapper;
        _grpcUserClient = grpcUserClient;
    }

    public async Task<Result<List<SimpleRecipeResponse>?>> Handle(GetSimpleRecipesQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var recipeIds = request.RecipeIds;

        if (accountId == Guid.Empty || recipeIds == null || recipeIds.Count == 0)
        {
            return Result<List<SimpleRecipeResponse>?>.Failure(RecipeError.NotFound, "AccountId and RecipeIds cannot be null or empty.");
        }
        var recipes = await _context.Recipes.Where(r => recipeIds.Contains(r.Id)).Select(r => new SimpleRecipeResponse
        {
            Id = r.Id,
            AuthorId = r.AuthorId,
            Title = r.Title,
            Description = r.Description,
            RecipeImgUrl = r.ImageUrl,
            VoteDiff = r.VoteDiff,
            NumberOfComment = r.NumberOfComment,
            Vote = Vote.None,
            AuthorUsername = "",
            AuthorAvtUrl = "",
            AuthorDisplayName = "",
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        }).ToListAsync();

        if (recipes == null || recipes.Count == 0)
        {
            return Result<List<SimpleRecipeResponse>?>.Success([]);

        }

        var recipeMap = await _context.Recipes.Where(r => recipeIds.Contains(r.Id)).ToDictionaryAsync(r => r.Id);
        var voteDict = new Dictionary<Guid, Vote>();
        foreach (var (key, value) in recipeMap)
        {
            var vote = recipeMap[key].RecipeVotes.Where(v => v.AccountId == accountId).SingleOrDefault();
            voteDict.Add(key, vote != null ? (vote.IsUpvote ? Vote.Upvote : Vote.Downvote) : Vote.None);
        }

        foreach (var recipe in recipes)
        {
            recipe.Vote = voteDict[recipe.Id];
        }

        var authorIds = recipes
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
                AccountUsername = value.AccountUsername,
            };
        }

        if (response == null || mapUsers.Count != authorIds.Count)
        {
            return Result<List<SimpleRecipeResponse>?>.Failure(RecipeError.NotFound, "Not found simple user.");
        }

        foreach (var recipe in recipes)
        {
            recipe.AuthorDisplayName = mapUsers[recipe.AuthorId].DisplayName;
            recipe.AuthorAvtUrl = mapUsers[recipe.AuthorId].AvtUrl;
            recipe.AuthorUsername = mapUsers[recipe.AuthorId].AccountUsername;
        }
        return Result<List<SimpleRecipeResponse>?>.Success(recipes);
    }
}
