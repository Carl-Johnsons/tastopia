using MongoDB.Bson.Serialization.Attributes;

namespace TrackingService.Domain.Common;

public class BaseEntity
{
    [BsonId]
    public Guid Id { get; set; }
}
