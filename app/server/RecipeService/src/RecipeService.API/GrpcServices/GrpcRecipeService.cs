using AutoMapper;
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

    public GrpcRecipeService(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
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
}
