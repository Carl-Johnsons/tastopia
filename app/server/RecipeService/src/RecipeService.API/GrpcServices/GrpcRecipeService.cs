using AutoMapper;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Newtonsoft.Json;
using RecipeProto;
using RecipeService.Application.Recipes.Queries;
using RecipeService.Application.Tags.Queries;

namespace RecipeService.API.GrpcServices;

public class GrpcRecipeService : GrpcRecipe.GrpcRecipeBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly ILogger<GrpcRecipeService> _logger;

    public GrpcRecipeService(ISender sender, IMapper mapper, ILogger<GrpcRecipeService> logger)
    {
        _sender = sender;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<GrpcListTagDTO> GetAllTags(GrpcEmpty request, ServerCallContext context)
    {
        var response = await _sender.Send(new GetAllTagsQuery());
        response.ThrowIfFailure();
        var result = new GrpcListTagDTO();
        foreach (var t in response.Value)
        {
            result.Tags.Add(_mapper.Map<GrpcTagDTO>(t));
        }
        _logger.LogInformation("Grpc GetAllTags successfully!");
        return result;
    }

    public override async Task<GrpcRecipeDetailsDTO> GetRecipeDetails(GrpcRecipeIdRequest request, ServerCallContext context)
    {
        var response = await _sender.Send(new GetRecipeDetailForServerQuery
        {
            RecipeId = Guid.Parse(request.RecipeId)
        });
        response.ThrowIfFailure();

        var result = _mapper.Map<GrpcRecipeDetailsDTO>(response.Value!.Recipe);

        _logger.LogInformation("Grpc GetRecipeDetails successfully!");
        return result;
    }

    public override async Task<GrpcMapSimpleRecipes> GetSimpleRecipes(GrpcGetSimpleRecipeRequest request, ServerCallContext context)
    {
        if (request.AccountId == null || request.RecipeIds == null || request.RecipeIds.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "RecipeIds and AccountId must not be null or empty."));
        }
        var recipeIdSet = request.RecipeIds.Select(Guid.Parse).ToHashSet();
        var accountId = Guid.Parse(request.AccountId);

        if (accountId == Guid.Empty || recipeIdSet == null || recipeIdSet.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "RecipeIds and AccountId must not be null or empty."));
        }

        var response = await _sender.Send(new GetSimpleRecipesQuery
        {
            AccountId = accountId,
            RecipeIds = recipeIdSet,
        });
        response.ThrowIfFailure();

        var recipes = response.Value!;

        var mapRecipe = recipes.ToDictionary(r => r.Id);

        var mapField = new MapField<string, GrpcSimpleRecipe>();
        foreach (var (key, value) in mapRecipe)
        {
            mapField[key.ToString()] = new GrpcSimpleRecipe
            {
                Id = value.Id.ToString(),
                AuthorId = value.AuthorId.ToString(),
                AuthorAvtUrl = value.AuthorAvtUrl,
                AuthorDisplayName = value.AuthorDisplayName,
                Title = value.Title,
                Description = value.Description,
                RecipeImgUrl = value.RecipeImgUrl,
                NumberOfComment = value.NumberOfComment,
                VoteDiff = value.VoteDiff,
                Vote = value.Vote.ToString(),
                CreatedAt = value.CreatedAt.ToTimestamp(),
                UpdatedAt = value.UpdatedAt.ToTimestamp()
            };
        }

        var grpcResult = new GrpcMapSimpleRecipes
        {
            Recipes = { mapField }
        };

        _logger.LogInformation("Grpc GetSimpleRecipes successfully!");
        return grpcResult;
    }

    public override async Task<GrpcMapSimpleRecipes> SearchSimpleRecipes(GrpcSearchSimpleRecipeRequest request, ServerCallContext context)
    {
        if (request.AccountId == null || request.RecipeIds == null || request.RecipeIds.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "RecipeIds and AccountId must not be null or empty."));
        }
        var recipeIdSet = request.RecipeIds.Select(Guid.Parse).ToHashSet();
        var accountId = Guid.Parse(request.AccountId);

        if (accountId == Guid.Empty || recipeIdSet == null || recipeIdSet.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "RecipeIds and AccountId must not be null or empty."));
        }

        var response = await _sender.Send(new SearchSimpleRecipesQuery
        {
            AccountId = accountId,
            RecipeIds = recipeIdSet,
            Keyword = request.Keyword
        });
        response.ThrowIfFailure();

        var recipes = response.Value!;

        var mapRecipe = recipes.ToDictionary(r => r.Id);

        var mapField = new MapField<string, GrpcSimpleRecipe>();
        foreach (var (key, value) in mapRecipe)
        {
            mapField[key.ToString()] = new GrpcSimpleRecipe
            {
                Id = value.Id.ToString(),
                AuthorId = value.AuthorId.ToString(),
                AuthorAvtUrl = value.AuthorAvtUrl,
                AuthorDisplayName = value.AuthorDisplayName,
                Title = value.Title,
                Description = value.Description,
                RecipeImgUrl = value.RecipeImgUrl,
                NumberOfComment = value.NumberOfComment,
                VoteDiff = value.VoteDiff,
                Vote = value.Vote.ToString(),
            };
        }

        var grpcResult = new GrpcMapSimpleRecipes
        {
            Recipes = { mapField }
        };

        _logger.LogInformation("Grpc SearchSimpleRecipes successfully!");
        return grpcResult;
    }

    public override Task<GrpcMapSimpleComments> GetSimpleComments(GrpcGetSimpleCommentRequest request, ServerCallContext context)
    {
        return base.GetSimpleComments(request, context);
    }
}
