using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("CommentVote")]
[PrimaryKey(nameof(CommentId), nameof(AccountId))]
public class CommentVote
{
    [Required]
    public Guid CommentId { get; set; }

    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public bool IsUpvote { get; set; } = true;

    public virtual Comment? Comment { get; set; }

}
