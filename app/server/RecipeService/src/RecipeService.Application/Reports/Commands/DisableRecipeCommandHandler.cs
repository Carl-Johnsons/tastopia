using Contract.Constants;
using Contract.Event.NotificationEvent;
using Contract.Event.TrackingEvent;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        var bin = await _context.UserRecipeBins.SingleOrDefaultAsync(b => b.RecipeId == recipe.Id && b.AccountId == recipe.AuthorId);
        if (bin != null)
        {
            _context.UserRecipeBins.Remove(bin);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
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
            ActivityType = ActivityType.DISABLE,
            EntityId = request.Id,
            EntityType = ActivityEntityType.RECIPE
        });

        await _serviceBus.Publish(new NotifyUserEvent
        {
            PrimaryActors = [
                new ActorDTO
                {
                    ActorId = recipe.Id.ToString(),
                    Type = EntityType.RECIPE
                }],
            SecondaryActors = [],
            TemplateCode = NotificationTemplateCode.ADMIN_DISABLE_RECIPE,
            Channels = [NOTIFICATION_CHANNEL.DEFAULT],
            JsonData = JsonConvert.SerializeObject(new
            {
                redirectUri = $"{CLIENT_URI.MOBILE.NOTIFICATION}"
            }),
            ImageUrl = recipe.ImageUrl,
            RecipientIds = [recipe.AuthorId]
        });

        return Result.Success();
    }
}
