using MassTransit;
using System.ComponentModel.DataAnnotations;
namespace Contract.Event.RecipeEvent;
[EntityName("UpdateRecipeIsActiveEvent")]
public class UpdateRecipeIsActiveEvent
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public bool IsActive { get; set; }
}
