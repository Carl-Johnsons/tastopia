using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("UserBookmarkRecipe")]
[PrimaryKey(nameof(AccountId), nameof(RecipeId))]
public class UserBookmarkRecipe
{
    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public Guid RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
