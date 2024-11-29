using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("RecipeVote")]
[PrimaryKey(nameof(RecipeId), nameof(UserId), nameof(VoteId))]
public class RecipeVote
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid VoteId { get; set; }

    public virtual Recipe? Recipe { get; set; }
    public virtual Vote? Vote { get; set; }
}
