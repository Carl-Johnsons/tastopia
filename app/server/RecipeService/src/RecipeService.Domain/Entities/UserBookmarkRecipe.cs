using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("UserBookmarkRecipe")]
[PrimaryKey(nameof(UserId), nameof(RecipeId))]
public class UserBookmarkRecipe
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
