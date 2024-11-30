using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("Comment")]
public class Comment : BaseAuditableEntity
{
    [Required]
    public string Content { get; set; } = null!;

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid RecipeId { get; set; }
    public virtual Recipe? Recipe { get; set; }
}
