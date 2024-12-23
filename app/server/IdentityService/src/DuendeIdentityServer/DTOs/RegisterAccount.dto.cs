
using System.ComponentModel.DataAnnotations;

namespace DuendeIdentityServer.DTOs;

public class RegisterAccountDTO
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Phone { get; set; } = null!;
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
