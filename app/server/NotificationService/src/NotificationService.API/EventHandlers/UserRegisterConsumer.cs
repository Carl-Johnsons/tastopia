using Contract.Constants;
using Contract.Event.IdentityEvent;
using MassTransit;
using NotificationService.Application.Notifications.Commands;

namespace NotificationService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.USER_REGISTER_NOTIFICATION)]
public class UserRegisterConsumer : IConsumer<UserRegisterEvent>
{
    private readonly ISender _sender;

    public UserRegisterConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<UserRegisterEvent> context)
    {
        switch (context.Message.Method)
        {
            case AccountMethod.Email:
                await _sender.Send(new SendEmailCommand
                {
                    EmailTo = context.Message.Identifier,
                    Subject = "Verify your account",
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
}
