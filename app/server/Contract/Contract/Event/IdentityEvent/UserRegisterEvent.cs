using Contract.Constants;
using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.IdentityEvent;

public record UserRegisterEvent
{
    [Required]
    public Guid AccountId { get; set; }
    [Required]
    public string Identifier { get; set; } = null!;
    [Required]
    public string OTP { get; set; } = null!;
    [Required]
    public AccountMethod Method { get; set; }
}
