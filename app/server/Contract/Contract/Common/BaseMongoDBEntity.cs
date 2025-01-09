using MongoDB.Bson.Serialization.Attributes;

namespace Contract.Common;

public class BaseMongoDBEntity
{
    [BsonId]
    public Guid Id { get; set; }
}
