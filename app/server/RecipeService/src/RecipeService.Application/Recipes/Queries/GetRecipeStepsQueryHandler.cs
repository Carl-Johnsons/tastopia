using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Recipes.Queries;

public class GetRecipeStepsQuery : IRequest<Result<List<Step>?>>
{
    [Required]
    public Guid RecipeId { get; init; }
}

public class GetRecipeStepsQueryHandler : IRequestHandler<GetRecipeStepsQuery, Result<List<Step>?>>
{
    private readonly IApplicationDbContext _context;
    public GetRecipeStepsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Step>?>> Handle(GetRecipeStepsQuery request, CancellationToken cancellationToken)
    {
        var recipeId = request.RecipeId;

        if(recipeId == Guid.Empty)
        {
            return Result<List<Step>?>.Failure(RecipeError.NotFound);
        }

        var recipe = await _context.Recipes
                .SingleOrDefaultAsync(r => r.Id == request.RecipeId);

        if (recipe == null)
        {
            return Result<List<Step>?>.Failure(RecipeError.NotFound);
        }

        recipe.Steps = recipe.Steps.OrderBy(s => s.OrdinalNumber).ToList();

        var result = recipe.Steps;
        return Result<List<Step>?>.Success(result);
    }
}
