using Contract.Constants;
using Contract.Event.TrackingEvent;
using MassTransit;
using TrackingService.Application.UserViewRecipeDetails.Commands;

namespace TrackingService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.CREATE_USER_VIEW_RECIPE_DETAIL,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.CREATE_USER_VIEW_RECIPE_DETAIL)]
public class CreateUserViewRecipeDetailConsumer : IConsumer<CreateUserViewRecipeDetailEvent>
{
    private readonly ISender _sender;

    public CreateUserViewRecipeDetailConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<CreateUserViewRecipeDetailEvent> context)
    {
        var result = await _sender.Send(new CreateUserVewRecipeDetailCommand
        {
            AccountId = context.Message.AccountId,
            RecipeId = context.Message.RecipeId,
            ViewTime = context.Message.ViewTime,
        });
        result.ThrowIfFailure();
    }
}
