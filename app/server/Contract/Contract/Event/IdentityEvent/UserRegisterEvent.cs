using Contract.Constants;
using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.IdentityEvent;

[EntityName("UserRegisterEvent")]
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
    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    public string AccountUsername { get; set; } = null!;
}
