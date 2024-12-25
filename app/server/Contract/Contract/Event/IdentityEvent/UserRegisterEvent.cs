using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.IdentityEvent;

[EntityName("user-register-event")]

public record UserRegisterEvent
{
    [Required]
    public Guid AccountId { get; set; }
    [Required]
    public string Phone { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string EmailOTP { get; set; } = null!;
}
