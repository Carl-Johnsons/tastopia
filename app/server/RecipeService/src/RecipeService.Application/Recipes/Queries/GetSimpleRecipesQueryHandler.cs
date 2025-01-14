using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;

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

    public GetSimpleRecipesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<SimpleRecipeResponse>?>> Handle(GetSimpleRecipesQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var recipeIds = request.RecipeIds;

        if(accountId == Guid.Empty || recipeIds == null || recipeIds.Count == 0)
        {
            return Result<List<SimpleRecipeResponse>?>.Failure(RecipeError.NotFound, "AccountId and RecipeIds cannot be null or empty.");
        }

        var recipes = await _context.Recipes.Where(r => recipeIds.Contains(r.Id)).Select(r => _mapper.Map<SimpleRecipeResponse>(r)).ToListAsync();

        await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(recipes, Formatting.Indented));

        return Result<List<SimpleRecipeResponse>?>.Failure(RecipeError.NotFound, "AccountId and RecipeIds cannot be null or empty.");

    }
}
