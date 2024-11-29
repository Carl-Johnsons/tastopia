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
}
