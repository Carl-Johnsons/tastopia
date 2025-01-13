using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.RecipeEvent;

[EntityName("ValidateRecipeEvent")]
public class ValidateRecipeEvent
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public List<string> TagValues { get; set; } = null!;

}
