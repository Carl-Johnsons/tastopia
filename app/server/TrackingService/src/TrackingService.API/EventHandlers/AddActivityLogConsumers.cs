using Contract.Constants;
using Contract.Event.TrackingEvent;
using MassTransit;
using TrackingService.Application.ActivityLog.Commands;

namespace TrackingService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.ADD_ACTIVITY_LOG,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.ADD_ACTIVITY_LOG)]
public class AddActivityLogConsumers : IConsumer<AddActivityLogEvent>
{
    private readonly ISender _sender;

    public AddActivityLogConsumers(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<AddActivityLogEvent> context)
    {
        var result = await _sender.Send(new AddActivityLogCommand
        {
            AccountId = context.Message.AccountId,
            ActivityType = context.Message.ActivityType,
            EntityId = context.Message.EntityId,
            EntityType = context.Message.EntityType,
            SecondaryEntityId = context.Message.SecondaryEntityId,
            SecondaryEntityType = context.Message.SecondaryEntityType
        });

        result.ThrowIfFailure();
    }
}
