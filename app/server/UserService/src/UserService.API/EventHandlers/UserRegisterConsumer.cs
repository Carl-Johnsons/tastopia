using Contract.Common;
using Contract.Event.IdentityEvent;
using MassTransit;
using UserService.Application.Users.Commands;
using UserService.Domain.Entities;

namespace UserService.API.EventHandlers;

[QueueName("user-register-event")]
public sealed class UserRegisterConsumer : IConsumer<UserRegisterEvent>
{
    private readonly ISender _sender;

    public UserRegisterConsumer(ISender sender)
    {
        _sender = sender;
    }


    public async Task Consume(ConsumeContext<UserRegisterEvent> context)
    {
        var accountId = context.Message.AccountId;
        var defaultAvatar = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png";
        var defaultBackground = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png";

        var user = new User {
            AccountId = accountId,
            AvatarUrl = defaultAvatar,
            BackgroundUrl = defaultBackground,
            DisplayName = context.Message.FullName,
            IsAccountActive = true,
            AccountUsername = context.Message.AccountUsername,
        };

        var response = await _sender.Send(new CreateUserCommand
        {
            User = user,
        });

        response.ThrowIfFailure();
    }
}