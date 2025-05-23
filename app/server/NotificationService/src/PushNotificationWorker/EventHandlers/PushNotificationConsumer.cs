﻿using Contract.Common;
using Contract.Constants;
using Contract.Event.NotificationEvent;
using MassTransit;
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
        var message = context.Message;

        await _pushNotificationService.Notify(message.ExpoPushTokens,
                                              message.Message,
                                              data: message.JsonData,
                                              title: message.Title,
                                              channelId: message.ChannelId);

        _logger.LogInformation("Push notification message acknowledged");
    }
}
