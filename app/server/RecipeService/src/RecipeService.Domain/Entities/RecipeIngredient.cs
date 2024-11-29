using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("RecipeIngredient")]
[PrimaryKey(nameof(RecipeId), nameof(IngredientId))]
public class RecipeIngredient
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public Guid IngredientId { get; set; }

    public virtual Recipe? Recipe { get; set; }
    public virtual Ingredient? Ingredient { get; set; }

}
