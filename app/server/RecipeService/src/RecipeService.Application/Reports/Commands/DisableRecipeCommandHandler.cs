using Contract.Event.TrackingEvent;
using RecipeService.Domain.Errors;

namespace RecipeService.Application.Reports.Commands;

public record DisableRecipeCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public Guid CurrentAccountId { get; set; }
}

public class DisableRecipeCommandHandler : IRequestHandler<DisableRecipeCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;

    public DisableRecipeCommandHandler(IApplicationDbContext context,
                                            IUnitOfWork unitOfWork,
                                            IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(DisableRecipeCommand request,
                               CancellationToken cancellationToken)
    {
        var recipe = _context.Recipes.SingleOrDefault(r => r.Id == request.Id);

        if (recipe == null)
        {
            return Result.Failure(RecipeError.NotFound);
        }

        if (!recipe.IsActive)
        {
            return Result.Failure(RecipeError.AlreadyInactive);
        }

        recipe.IsActive = false;

        _context.Recipes.Update(recipe);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        await _serviceBus.Publish(new AddActivityLogEvent
        {
            AccountId = request.CurrentAccountId,
            ActivityType = Contract.Constants.ActivityType.DISABLE,
            EntityId = request.Id,
            EntityType = Contract.Constants.ActivityEntityType.RECIPE
        });

        return Result.Success();
    }
}
