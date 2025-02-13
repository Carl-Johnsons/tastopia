using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.TrackingEvent;

[EntityName("CreateUserSearchRecipeEvent")]
public class CreateUserSearchRecipeEvent
{
    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public string Keyword { get; set; } = null!;

    [Required]
    public DateTime SearchTime { get; set; }
}
