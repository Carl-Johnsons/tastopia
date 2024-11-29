
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("Ingredient")]
public class Ingredient : BaseAuditableEntity
{
    [Required]
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
}
