using MassTransit;

namespace Contract.Event.UserEvent;

[EntityName("get-simple-users-event")]
public record GetSimpleUsersEvent
{
    public HashSet<Guid> UserIds { get; set; } = null!;
}
