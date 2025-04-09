using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RecipeService.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Entities;

[Collection("Tag")]
public class Tag : BaseMongoDBAuditableEntity
{
    [Required]
    public TagValue Value { get; set; } = null!;

    [Required]
    public string Code { get; set; } = null!;

    [Required]
    [JsonConverter(typeof(StringEnumConverter))]
    [BsonRepresentation(BsonType.String)]
    public TagCategory Category { get; set; } = TagCategory.All;

    [Required]
    [JsonConverter(typeof(StringEnumConverter))]
    [BsonRepresentation(BsonType.String)]
    public TagStatus Status { get; set; } = TagStatus.Active;

    [Required]
    public string ImageUrl { get; set; } = null!;

    public virtual List<RecipeTag> RecipeTags { get; set; } = new();

}
public class TagValue
{
    public string En { get; set; } = null!;
    public string Vi { get; set; } = null!;
}
