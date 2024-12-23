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

        await _sender.Send(new SendEmailCommand
        {
            EmailTo = context.Message.Email,
            Subject = "Verify your account",
            Body = $"Your <b>Tastopia</b> account is create. Your OTP to verify is <b>{context.Message.EmailOTP}</b>",
            IsHTML = true,
        });

    }
}
