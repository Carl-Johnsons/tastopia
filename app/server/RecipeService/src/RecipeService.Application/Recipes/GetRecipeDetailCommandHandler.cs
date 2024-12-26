using MassTransit;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Recipes;

public class GetRecipeDetailCommand : IRequest<Result<Recipe?>>
{
    [Required]
    public Guid RecipeId { get; init; }
}

public class GetRecipeDetailCommandHandler : IRequestHandler<GetRecipeDetailCommand, Result<Recipe?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IBus _bus;

    public GetRecipeDetailCommandHandler(IApplicationDbContext context, IBus bus)
    {
        _context = context;
        _bus = bus;
    }

    public async Task<Result<Recipe?>> Handle(GetRecipeDetailCommand request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
            .Include(r => r.Steps)
            .Include(r => r.Comments)
            .Where(r => r.Id == request.RecipeId).FirstOrDefaultAsync();

        if(recipe == null)
        {
            return Result<Recipe?>.Failure(RecipeError.NotFound);
        }

        return Result<Recipe?>.Failure(RecipeError.NotFound);

    }
}
