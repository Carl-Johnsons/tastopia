using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("CommentVote")]
[PrimaryKey(nameof(CommentId), nameof(UserId))]
public class CommentVote
{
    [Required]
    public Guid CommentId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public bool IsUpvote { get; set; } = true;

    public virtual Comment? Comment { get; set; }

}
