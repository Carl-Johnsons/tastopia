using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("Comment")]
public class Comment : BaseAuditableEntity
{
    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = null!;

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

    public virtual Recipe? Recipe { get; set; }
    public virtual List<CommentVote>? CommentVotes { get; set; }
}
