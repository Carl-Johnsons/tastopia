using MassTransit;

namespace Contract.Event.UserEvent;

[EntityName("get-user-details-event")]
public record GetUserDetailsEvent
{
    public Guid AccountId { get; set; }
}
