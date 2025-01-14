using AutoMapper;
using Google.Protobuf.Collections;
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
    private readonly ILogger _logger;

    public GrpcRecipeService(ISender sender, IMapper mapper, ILogger logger)
    {
        _sender = sender;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<GrpcListTagDTO> GetAllTags(RecipeProto.GrpcEmpty request, ServerCallContext context)
    {
        var response = await _sender.Send(new GetAllTagsQuery());
        response.ThrowIfFailure();
        var result = new GrpcListTagDTO();
        foreach (var t in response.Value)
        {
            result.Tags.Add(_mapper.Map<GrpcTagDTO>(t));
        }
        await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
        return result;
    }

    public override async Task<GrpcRecipeDetailsDTO> GetRecipeDetails(GrpcRecipeIdRequest request, ServerCallContext context)
    {
        var response = await _sender.Send(new GetRecipeDetailQuery
        {
            RecipeId = Guid.Parse(request.RecipeId),
        });
        response.ThrowIfFailure();

        var result = _mapper.Map<GrpcRecipeDetailsDTO>(response.Value!.Recipe);
        await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
        return result;
    }

    public override async Task<GrpcMapSimpleRecipes> GetSimpleRecipes(GrpcGetSimpleRecipeRequest request, ServerCallContext context)
    {
        if (request.AccountId == null || request.RecipeIds == null ||request.RecipeIds.Count == 0)
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
            mapField[key.ToString()] = _mapper.Map<GrpcSimpleRecipe>(value);
        }

        var grpcResult = new GrpcMapSimpleRecipes
        {
            Recipes = { mapField }
        };
        _logger.LogInformation(JsonConvert.SerializeObject(grpcResult, Formatting.Indented));
        return grpcResult;
    }
}
