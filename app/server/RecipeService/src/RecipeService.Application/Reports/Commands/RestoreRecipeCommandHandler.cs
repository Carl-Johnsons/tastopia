using RecipeService.Domain.Errors;

namespace RecipeService.Application.Reports.Commands;

public record RestoreRecipeCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}

public class RestoreRecipeCommandHandler : IRequestHandler<RestoreRecipeCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public RestoreRecipeCommandHandler(IApplicationDbContext context,
                                            IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RestoreRecipeCommand request,
                               CancellationToken cancellationToken)
    {
        var recipe = _context.Recipes.SingleOrDefault(r => r.Id == request.Id);

        if (recipe == null)
        {
            return Result.Failure(RecipeError.NotFound);
        }

        if (recipe.IsActive)
        {
            return Result.Failure(RecipeError.AlreadyActive);
        }

        recipe.IsActive = true;

        _context.Recipes.Update(recipe);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Success();
    }
}
