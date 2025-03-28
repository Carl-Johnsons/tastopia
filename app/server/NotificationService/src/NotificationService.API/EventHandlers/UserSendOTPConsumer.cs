using Contract.Constants;
using Contract.Event.IdentityEvent;
using MassTransit;
using NotificationService.Application.Notifications.Commands;

namespace NotificationService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.USER_RESEND_OTP,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.USER_RESEND_OTP)]
public class UserSendOTPConsumer : IConsumer<UserSendOTPEvent>
{
    private readonly ISender _sender;

    public UserSendOTPConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<UserSendOTPEvent> context)
    {
        switch (context.Message.OTPMethod)
        {
            case OTPMethod.Resend:
                await ResendOTP(context);
                break;
            case OTPMethod.ForgotPassword:
                await ForgotPassword(context);
                break;
            default:
                break;
        }
    }

    public async Task ResendOTP(ConsumeContext<UserSendOTPEvent> context)
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
                await _sender.Send(new SendSMSCommand
                {
                    Message = $"Your Tastopia account is create. Your OTP to verify is {context.Message.OTP}",
                    PhoneTo = context.Message.Identifier,
                });
                break;
            default:
                break;
        }
    }
    public async Task ForgotPassword(ConsumeContext<UserSendOTPEvent> context)
    {
        switch (context.Message.Method)
        {
            case AccountMethod.Email:
                await _sender.Send(new SendEmailCommand
                {
                    EmailTo = context.Message.Identifier,
                    Subject = "Reset Password",
                    Body = $"Your <b>Tastopia</b> account request to reset the password. Your OTP to verify reset password is <b>{context.Message.OTP}</b>",
                    IsHTML = true,
                });
                break;
            case AccountMethod.Phone:
                await _sender.Send(new SendSMSCommand
                {
                    Message = $"Your Tastopia account request to reset the password. Your OTP to verify reset password is {context.Message.OTP}",
                    PhoneTo = context.Message.Identifier,
                });
                break;
            default:
                break;
        }
    }
}
