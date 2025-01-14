using AutoMapper;
using Google.Protobuf.Collections;
using Newtonsoft.Json;
using RecipeProto;
using TrackingService.Domain.Errors;

namespace TrackingService.Application.UserViewRecipeDetails.Queries;

public class GetUserViewRecipeDetaiQuery : IRequest<Result>
{
    public Guid AccountId { get; set; }
}

public class GetUserViewRecipeDetaiQueryHandler : IRequestHandler<GetUserViewRecipeDetaiQuery, Result>
{
    private readonly GrpcRecipe.GrpcRecipeClient _grpcRecipeClient;
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetUserViewRecipeDetaiQueryHandler(GrpcRecipe.GrpcRecipeClient grpcRecipeClient, IMapper mapper, IApplicationDbContext context)
    {
        _grpcRecipeClient = grpcRecipeClient;
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result> Handle(GetUserViewRecipeDetaiQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        if(accountId == Guid.Empty)
        {
            return Result.Failure(UserViewRecipeDetailError.NotFound, "ACCOUNT ID NULL");
        }

        var recipeIds = _context.UserViewRecipeDetails.Where(v => v.AccountId == accountId).OrderByDescending(v => v.UpdatedAt).ToHashSet();

        var response = await _grpcRecipeClient.GetSimpleRecipesAsync(new GrpcGetSimpleRecipeRequest
        {
            AccountId = accountId.ToString(),
            RecipeIds = { _mapper.Map<RepeatedField<string>>(recipeIds) }
        }, cancellationToken: cancellationToken);

        await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(response.Recipes, Formatting.Indented));
        return Result.Failure(UserViewRecipeDetailError.NotFound, "OKKKKKKKKKKK");
    }
}
