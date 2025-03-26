using Contract.Constants;
using MassTransit;

namespace Contract.Event.TrackingEvent;

[EntityName("AddActivityLogEvent")]
public class AddActivityLogEvent
{
    public Guid AccountId { get; set; }
    public ActivityType ActivityType { get; set; }
    public Guid EntityId { get; set; }
    public ActivityEntityType EntityType { get; set; }
    public Guid? SecondaryEntityId { get; set; }
    public ActivityEntityType? SecondaryEntityType { get; set; }
}
