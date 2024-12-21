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

    public async Task Consume(ConsumeContext<UserRegisterEvent> context)
    {
        var accountId = context.Message.AccountId;

        var user = new User { AccountId = accountId };

        var response = await _sender.Send(new CreateUserCommand
        {
            User = user,
        });

        response.ThrowIfFailure();
    }
}