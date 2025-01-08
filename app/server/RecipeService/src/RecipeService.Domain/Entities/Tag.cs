using MongoDB.EntityFrameworkCore;
using Newtonsoft.Json;
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
    public string Category { get; set; } = null!;

    [Required]
    public TagStatus Status { get; set; } = TagStatus.Active;

    [Required]
    public string ImageUrl { get; set; } = null!;

    public virtual List<RecipeTag> RecipeTags { get; set; } = new();

}
