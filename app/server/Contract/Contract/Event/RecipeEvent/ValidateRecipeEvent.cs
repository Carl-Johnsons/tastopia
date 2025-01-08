using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.RecipeEvent;

[EntityName("ValidateRecipeEvent")]
public class ValidateRecipeEvent
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public List<string> TagCodes { get; set; } = null!;

    [Required]
    public List<string> AdditionTagValues { get; set; } = null!;
}
