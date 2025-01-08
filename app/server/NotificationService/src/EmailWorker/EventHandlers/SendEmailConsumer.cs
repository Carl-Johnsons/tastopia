using Contract.Common;
using Contract.Constants;
using Contract.Event.NotificationEvent;
using EmailWorker.Interfaces;
using MassTransit;

namespace EmailWorker.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.SEND_EMAIL,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.SEND_EMAIL)]
public class SendEmailConsumer : IConsumer<SendEmailEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<SendEmailConsumer> _logger;

    public SendEmailConsumer(IEmailService emailService, ILogger<SendEmailConsumer> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SendEmailEvent> context)
    {
        await _emailService.SendEmail(
            emailTo: context.Message.EmailTo,
            subject: context.Message.Subject,
            body: context.Message.Body,
            isHtml: true);

        _logger.LogInformation("Message acknowledged");
    }
}
