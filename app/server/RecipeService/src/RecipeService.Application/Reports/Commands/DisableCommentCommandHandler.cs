using Contract.Constants;
using Contract.Event.NotificationEvent;
using Contract.Event.TrackingEvent;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;

namespace RecipeService.Application.Reports.Commands;

public record DisableCommentCommand : IRequest<Result>
{
    public Guid CommentId { get; set; }
    public Guid RecipeId { get; set; }
    public Guid CurrentAccountId { get; set; }
}

public class DisableCommentCommandHandler : IRequestHandler<DisableCommentCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;

    public DisableCommentCommandHandler(IApplicationDbContext context,
                                            IUnitOfWork unitOfWork,
                                            IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(DisableCommentCommand request,
                               CancellationToken cancellationToken)
    {
        var recipeCollection = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe));

        var recipeFilter = Builders<Recipe>.Filter.Eq(r => r.Id, request.RecipeId);

        var commentProjection = Builders<Recipe>.Projection.Expression(r =>
            r.Comments.FirstOrDefault(c => c.Id == request.CommentId));

        var recipeQuery = recipeCollection
            .Find(recipeFilter);

        var comment = await recipeQuery
            .Project(commentProjection)
            .FirstOrDefaultAsync(cancellationToken);

        var recipe = await recipeQuery.FirstOrDefaultAsync(cancellationToken);

        if (comment == null)
        {
            return Result.Failure(CommentError.NotFound);
        }

        if (!comment.IsActive)
        {
            return Result.Failure(CommentError.AlreadyInactive);
        }

        var filter = Builders<Recipe>.Filter.And(
            Builders<Recipe>.Filter.Eq(r => r.Id, request.RecipeId),
            Builders<Recipe>.Filter.Eq("Comments.Id", request.CommentId)
        );

        var update = Builders<Recipe>.Update.Set("Comments.$.IsActive", false);

        var updateResult = await recipeCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        if (updateResult.ModifiedCount == 0)
        {
            return Result.Failure(CommentError.NotFound);
        }

        await _serviceBus.Publish(new AddActivityLogEvent
        {
            AccountId = request.CurrentAccountId,
            ActivityType = ActivityType.DISABLE,
            EntityId = request.CommentId,
            EntityType = ActivityEntityType.COMMENT,
            SecondaryEntityId = request.RecipeId,
            SecondaryEntityType = ActivityEntityType.RECIPE
        });

        await _serviceBus.Publish(new NotifyUserEvent
        {
            PrimaryActors = [
                new ActorDTO
                {
                    ActorId = request.RecipeId + "~" + request.CommentId,
                    Type = EntityType.COMMENT
                }],
            SecondaryActors = [],
            TemplateCode = NotificationTemplateCode.ADMIN_DISABLE_COMMENT,
            Channels = [NOTIFICATION_CHANNEL.DEFAULT],
            JsonData = JsonConvert.SerializeObject(new
            {
                redirectUri = $"{CLIENT_URI.MOBILE.NOTIFICATION}"
            }),
            ImageUrl = recipe.ImageUrl,
            RecipientIds = [comment.AccountId]
        });

        return Result.Success();
    }
}
