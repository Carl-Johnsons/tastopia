using MassTransit;

namespace Contract.Event.UserEvent;

[EntityName("GetUserDetailsEvent")]
public record GetUserDetailsEvent
{
    public Guid AccountId { get; set; }
}
