using Contract.Common;
using Contract.Constants;
using Contract.Event.NotificationEvent;
using MassTransit;
using Newtonsoft.Json;
using PushNotificationWorker.Interfaces;

namespace PushNotificationWorker.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.PUSH_NOTIFICATION)]
public class PushNotificationConsumer : IConsumer<PushNotificationEvent>
{
    private readonly IPushNotificationService _pushNotificationService;
    private readonly ILogger<PushNotificationConsumer> _logger;

    public PushNotificationConsumer(IPushNotificationService pushNotificationService, ILogger<PushNotificationConsumer> logger)
    {
        _pushNotificationService = pushNotificationService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PushNotificationEvent> context)
    {
        await _pushNotificationService.Notify(context.Message.ExpoPushTokens,
                                            context.Message.Message,
                                            context.Message.JsonData);

        _logger.LogInformation("Push notification message acknowledged");
    }
}
