namespace DuendeIdentityServer.DTOs;

public class UpdateAdminAccountDTO
{
    public Guid AccountId { get; set; }
    public string? Username { get; set; } = null!;
    public string? Name { get; set; } = null!;
    public string? Gmail { get; set; } = null!;
    public string? Phone { get; set; } = null!;
    public string? Password { get; set; } = null!;
    public string? Gender { get; set; } = null!;
    public DateTime? Dob { get; set; }
    public string? Address { get; set; } = null!;
    public IFormFile? AvatarFile { get; set; } = null!;
}
