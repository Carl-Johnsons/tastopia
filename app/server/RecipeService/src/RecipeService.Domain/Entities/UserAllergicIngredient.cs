using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("UserAllericIngredient")]
[PrimaryKey(nameof(UserId), nameof(IngredientId))]
public class UserAllergicIngredient
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid IngredientId { get; set; }
    public virtual Ingredient? Ingredient { get; set; }
}
