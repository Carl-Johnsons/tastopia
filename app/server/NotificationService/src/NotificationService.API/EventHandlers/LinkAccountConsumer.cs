using Contract.Common;
using Contract.Constants;
using Contract.Event.IdentityEvent;
using MassTransit;
using NotificationService.Application.Emails;

namespace NotificationService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.LINK_ACCOUNT,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.LINK_ACCOUNT)]
public class LinkAccountConsumer : IConsumer<LinkAccountEvent>
{
    private readonly ISender _sender;

    public LinkAccountConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<LinkAccountEvent> context)
    {
        switch (context.Message.Method)
        {
            case AccountMethod.Email:
                await _sender.Send(new SendEmailCommand
                {
                    EmailTo = context.Message.Identifier,
                    Subject = "Verify your account",
                    Body = $"You have linked your email to <b>Tastopia</b> account. Your OTP to verify is <b>{context.Message.OTP}</b>",
                    IsHTML = true,
                });
                break;
            case AccountMethod.Phone:
                break;
            default:
                break;
        }
    }
}
