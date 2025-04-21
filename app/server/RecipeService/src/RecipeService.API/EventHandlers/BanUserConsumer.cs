using Contract.Constants;
using Contract.Event.RecipeEvent;
using Contract.Event.UserEvent;
using MassTransit;
using RecipeService.Application.Recipes.Commands;

namespace RecipeService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.BAN_USER,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.BAN_USER)]
public class BanUserConsumer : IConsumer<BanUserEvent>
{
    private readonly ISender _sender;

    public BanUserConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<BanUserEvent> context)
    {
        var result = await _sender.Send(new DisableUserRecipeCommand
        {
            AccountId = context.Message.AccountId,
        });
        result.ThrowIfFailure();
    }
}
