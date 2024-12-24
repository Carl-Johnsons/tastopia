using Contract.Constants;
using Contract.Event.IdentityEvent;
using MassTransit;
using NotificationService.Application.Emails;

namespace NotificationService.API.EventHandlers;

public class UserRegisterConsumer : IConsumer<UserRegisterEvent>
{
    private readonly ISender _sender;

    public UserRegisterConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<UserRegisterEvent> context)
    {
        Console.WriteLine(JsonConvert.SerializeObject(context.Message));
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
                break;
            default:
                break;
        }
    }
}
