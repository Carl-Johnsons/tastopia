using Contract.Constants;
using MongoDB.EntityFrameworkCore;

namespace TrackingService.Domain.Entities;

[Collection("AdminActivityLog")]
public class AdminActivityLog : BaseMongoDBAuditableEntity
{
    public Guid AccountId { get; set; }

    public ActivityType ActivityType { get; set; }
    public Guid EntityId { get; set; }
    public ActivityEntityType EntityType { get; set; }
    public Guid? SecondaryEntityId { get; set; }
    public ActivityEntityType? SecondaryEntityType { get; set; }
}
