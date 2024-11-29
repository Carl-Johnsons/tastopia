using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("RecipeStep")]
[PrimaryKey(nameof(RecipeId), nameof(StepId))]
public class RecipeStep
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public Guid StepId { get; set; }

    public virtual Recipe? Recipe { get; set; }
    public virtual Step? Step { get; set; }

}
