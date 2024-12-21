
namespace DuendeIdentityServer.DTOs;

public class ApplicationUserResponseDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public string? AvatarUrl { get; set; }

    public string? BackgroundUrl { get; set; }
    public string? Introduction { get; set; }
    public DateTime? Dob { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Gender { get; set; }
    public bool? Active { get; set; }
}
