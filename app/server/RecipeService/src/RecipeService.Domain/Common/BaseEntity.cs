using MongoDB.Bson.Serialization.Attributes;

namespace RecipeService.Domain.Common;

public class BaseEntity
{
    [BsonId]
    public Guid Id { get; set; }
}
