using Contract.Common;
using Contract.Event.IdentityEvent;
using MassTransit;
using UserService.Application.Users.Commands;
using UserService.Domain.Entities;

namespace UserService.API.EventHandlers;

[QueueName("user-register-event-queue")]
public sealed class UserRegisterConsumer : IConsumer<UserRegisterEvent>
{
    private readonly ISender _sender;

    public UserRegisterConsumer(ISender sender)
    {
        _sender = sender;
    }
    private static string GenerateRandomDisplayName()
    {
        string[] firstNames = { "John", "Jane", "Michael", "Emily", "Chris", "Emma", "James", "Olivia" };
        string[] lastNames = { "Doe", "Smith", "Johnson", "Brown", "Williams", "Taylor", "Davis", "Wilson" };

        Random random = new Random();

        string firstName = firstNames[random.Next(firstNames.Length)];
        string lastName = lastNames[random.Next(lastNames.Length)];

        return $"{firstName} {lastName}";
    }

    public async Task Consume(ConsumeContext<UserRegisterEvent> context)
    {
        var accountId = context.Message.AccountId;
        var defaultAvatar = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png";
        var defaultBackground = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png";

        var user = new User {
            Id = accountId,
            AvatarUrl = defaultAvatar,
            BackgroundUrl = defaultBackground,
            DisplayName = GenerateRandomDisplayName(),
        };

        var response = await _sender.Send(new CreateUserCommand
        {
            User = user,
        });

        response.ThrowIfFailure();
    }
}