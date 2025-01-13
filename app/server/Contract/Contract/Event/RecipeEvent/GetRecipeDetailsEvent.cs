using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.RecipeEvent;

[EntityName("GetRecipeDetailsEvent")]
public class GetRecipeDetailsEvent
{
    [Required]
    public Guid RecipeId { get; set; }
}
