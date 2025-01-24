using Contract.Constants;
using Contract.Event.IdentityEvent;
using MassTransit;
using NotificationService.Application.Notifications.Commands;

namespace NotificationService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.USER_RESEND_OTP,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.USER_RESEND_OTP)]
public class UserResendOTPConsumer : IConsumer<UserResendOTPEvent>
{
    private readonly ISender _sender;

    public UserResendOTPConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<UserResendOTPEvent> context)
    {
        switch (context.Message.Method)
        {
            case AccountMethod.Email:
                await _sender.Send(new SendEmailCommand
                {
                    EmailTo = context.Message.Identifier,
                    Subject = "Resend Verify your account",
                    Body = $"Your <b>Tastopia</b> account is create. Your OTP to verify is <b>{context.Message.OTP}</b>",
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
