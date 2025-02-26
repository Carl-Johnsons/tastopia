using Contract.Constants;
using Contract.Event.UserEvent;
using MassTransit;
using UserService.Application.Users.Commands;

namespace UserService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.UPDATE_TOTAL_RECIPE,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.UPDATE_TOTAL_RECIPE)]
public class UpdateTotalRecipeConsumer : IConsumer<UpdateUserTotalRecipeEvent>
{
    private readonly ISender _sender;

    public UpdateTotalRecipeConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<UpdateUserTotalRecipeEvent> context)
    {
        var result = await _sender.Send(new UpdateUserTotalRecipeCommand
        {
            AccountId = context.Message.AccountId,
            Delta = context.Message.Delta,
        });
        result.ThrowIfFailure();
    }
}
