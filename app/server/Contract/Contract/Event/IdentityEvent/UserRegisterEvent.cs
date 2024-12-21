using MassTransit;

namespace Contract.Event.IdentityEvent;

[EntityName("user-register-event")]

public record UserRegisterEvent
{
    public Guid AccountId { get; set; }
}
