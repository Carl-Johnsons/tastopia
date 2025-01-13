using Contract.Constants;
using Contract.Event.IdentityEvent;
using MassTransit;
using NotificationService.Application.Emails;

namespace NotificationService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.UNLINK_ACCOUNT,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.UNLINK_ACCOUNT)]
public class UnlinkAccountConsumer : IConsumer<UnlinkAccountEvent>
{
    private readonly ISender _sender;

    public UnlinkAccountConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<UnlinkAccountEvent> context)
    {
        switch (context.Message.Method)
        {
            case AccountMethod.Email:
                await _sender.Send(new SendEmailCommand
                {
                    EmailTo = context.Message.Identifier,
                    Subject = "Verify to unlink email",
                    Body = $"You have unlinked your email to <b>Tastopia</b> account. Your OTP to verify is <b>{context.Message.OTP}</b>",
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
