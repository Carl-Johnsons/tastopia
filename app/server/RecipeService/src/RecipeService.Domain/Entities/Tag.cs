using MongoDB.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RecipeService.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Entities;

[Collection("Tag")]
public class Tag : BaseAuditableEntity
{
    [Required]
    public string Value { get; set; } = null!;

    [Required]
    public string Code { get; set; } = null!;

    [Required]
    public TagCategory Category { get; set; } = TagCategory.All;

    [Required]
    [JsonConverter(typeof(StringEnumConverter))]
    public TagStatus Status { get; set; } = TagStatus.Active;

    [Required]
    public string ImageUrl { get; set; } = null!;

    public virtual List<RecipeTag> RecipeTags { get; set; } = new();

}
