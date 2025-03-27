using Contract.Common;
using Contract.Constants;
using Contract.Event.NotificationEvent;
using SMSWorker.Interfaces;
using MassTransit;

namespace SMSWorker.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.SEND_SMS)]
public class SendSMSConsumer : IConsumer<SendSMSEvent>
{
    private readonly ISMSService _smsService;
    private readonly ILogger<SendSMSConsumer> _logger;

    public SendSMSConsumer(ISMSService smsService, ILogger<SendSMSConsumer> logger)
    {
        _smsService = smsService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SendSMSEvent> context)
    {
        await _smsService.SendSMS(context.Message.PhoneTo, context.Message.Message);
        _logger.LogInformation("Send sms message acknowledged");
    }
}
