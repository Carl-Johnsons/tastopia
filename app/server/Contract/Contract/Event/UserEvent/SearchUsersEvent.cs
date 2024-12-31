using MassTransit;

namespace Contract.Event.UserEvent;

[EntityName("SearchUsersEvent")]
public class SearchUsersEvent
{
    public HashSet<Guid>? AccountIds { get; set; }
    public string? Keyword { get; set; } 
}
