using MassTransit;

namespace Contract.Event.UserEvent;

[EntityName("search-users-event")]
public class SearchUsersEvent
{
    public HashSet<Guid>? UserIds { get; set; }
    public string? Keyword { get; set; } 
}
