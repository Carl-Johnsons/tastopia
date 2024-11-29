using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("RecipeComment")]
[PrimaryKey(nameof(RecipeId), nameof(CommentId))]
public class RecipeComment
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public Guid CommentId { get; set; }

    public virtual Recipe? Recipe { get; set; }
    public virtual Comment? Comment { get; set; }
}
