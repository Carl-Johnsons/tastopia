using Contract.Event.IdentityEvent;
using MassTransit;

namespace NotificationService.API.EventHandlers;

public class UserRegisterConsumer : IConsumer<UserRegisterEvent>
{
    private readonly ISender _sender;

    public UserRegisterConsumer(ISender sender)
    {
        _sender = sender;
    }

    public Task Consume(ConsumeContext<UserRegisterEvent> context)
    {
        throw new NotImplementedException();
    }
}
