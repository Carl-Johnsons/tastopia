using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.IdentityEvent;

[EntityName("UpdateAccountIsActiveEvent")]
public class UpdateAccountIsActiveEvent
{
    [Required]
    public Guid AccountId { get; set; }
    [Required]
    public bool IsActive { get; set; } = true;
}
