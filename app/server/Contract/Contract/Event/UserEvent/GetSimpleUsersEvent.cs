using MassTransit;

namespace Contract.Event.UserEvent;

[EntityName("GetSimpleUsersEvent")]
public record GetSimpleUsersEvent
{
    public HashSet<Guid> AccountIds { get; set; } = null!;
}
