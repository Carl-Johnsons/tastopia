using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("RecipeVote")]
[PrimaryKey(nameof(RecipeId), nameof(UserId))]
public class RecipeVote
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public bool IsUpvote { get; set; } = true;
    public virtual Recipe? Recipe { get; set; }
}
