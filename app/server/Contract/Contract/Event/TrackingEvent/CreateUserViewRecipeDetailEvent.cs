using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.TrackingEvent;

[EntityName("CreateUserViewRecipeDetailEvent")]
public class CreateUserViewRecipeDetailEvent
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public DateTime ViewTime { get; set; }
}
