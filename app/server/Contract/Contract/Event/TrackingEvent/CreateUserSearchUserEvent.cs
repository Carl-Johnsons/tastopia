using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.TrackingEvent;

[EntityName("CreateUserSearchUserEvent")]
public class CreateUserSearchUserEvent
{
    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public string Keyword { get; set; } = null!;

    [Required]
    public DateTime SearchTime { get; set; }
}
