using Contract.Constants;
using MassTransit;
using IdentityService.Application.Account.Commands;
using Contract.Event.IdentityEvent;

namespace DuendeIdentityServer.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.UPDATE_ACCOUNT_IS_ACTIVE,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.UPDATE_ACCOUNT_IS_ACTIVE)]
public class UpdateAccountIsActiveConsumer : IConsumer<UpdateAccountIsActiveEvent>
{
    private readonly ISender _sender;
    public UpdateAccountIsActiveConsumer(ISender sender)
    {
        _sender = sender;
    }
    public async Task Consume(ConsumeContext<UpdateAccountIsActiveEvent> context)
    {
        var result = await _sender.Send(new UpdateAccountCommand
        {
            AccountId = context.Message.AccountId,
            IsActive = context.Message.IsActive,
        });
        result.ThrowIfFailure();
    }
}
