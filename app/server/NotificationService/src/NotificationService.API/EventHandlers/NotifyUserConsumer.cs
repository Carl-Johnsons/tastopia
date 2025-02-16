using Contract.Constants;
using Contract.Event.NotificationEvent;
using MassTransit;
using NotificationService.Application.Notifications.Commands;
using NotificationService.Domain.Entities;

namespace NotificationService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.NOTIFY_USER,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.NOTIFY_USER)]
public class NotifyUserConsumer : IConsumer<NotifyUserEvent>
{
    private readonly ISender _sender;

    public NotifyUserConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<NotifyUserEvent> context)
    {
        var message = context.Message;
        var pas = message.PrimaryActors.Select(pa => new Actor
        {
            ActorId = pa.ActorId,
            Type = pa.Type
        }).ToList();

        var sas = message.SecondaryActors.Select(sa => new Actor
        {
            ActorId = sa.ActorId,
            Type = sa.Type
        }).ToList();

        var command = new NotifyUserCommand
        {
            PrimaryActors = pas,
            SecondaryActors = sas,
            Channels = message.Channels,
            ImageUrl = message.ImageUrl,
            JsonData = message.JsonData,
            TemplateCode = message.TemplateCode
        };

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
    }
}
