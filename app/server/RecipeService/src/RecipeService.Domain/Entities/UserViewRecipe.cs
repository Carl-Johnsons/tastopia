using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RecipeService.Domain.Entities;

[Table("UserViewRecipe")]
[PrimaryKey(nameof(UserId), nameof(RecipeId))]
public class UserViewRecipe
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public DateTime LastestViewTime { get; set; }

    public int? NumberOfView {  get; set; }
    public virtual Recipe? Recipe { get; set; }
}
