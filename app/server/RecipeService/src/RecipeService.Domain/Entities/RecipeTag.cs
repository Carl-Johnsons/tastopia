using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("RecipeTag")]
[PrimaryKey(nameof(RecipeId), nameof(TagId))]
public class RecipeTag
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public Guid TagId { get; set; }

    public virtual Recipe? Recipe { get; set; }
    public virtual Tag? Tag { get; set; }

}
