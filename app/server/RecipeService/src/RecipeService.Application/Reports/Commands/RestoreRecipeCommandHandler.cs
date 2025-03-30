using Contract.Event.TrackingEvent;
using RecipeService.Domain.Errors;

namespace RecipeService.Application.Reports.Commands;

public record RestoreRecipeCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public Guid CurrentAccountId { get; set; }
}

public class RestoreRecipeCommandHandler : IRequestHandler<RestoreRecipeCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;

    public RestoreRecipeCommandHandler(IApplicationDbContext context,
                                            IUnitOfWork unitOfWork,
                                            IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
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

        await _serviceBus.Publish(new AddActivityLogEvent
        {
            AccountId = request.CurrentAccountId,
            ActivityType = Contract.Constants.ActivityType.RESTORE,
            EntityId = request.Id,
            EntityType = Contract.Constants.ActivityEntityType.RECIPE
        });

        return Result.Success();
    }
}
