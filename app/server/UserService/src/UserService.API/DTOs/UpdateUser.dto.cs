namespace UserService.API.DTOs;

public class UpdateUserDTO
{
    public string? DisplayName { get; set; } = null!;
    public string? Bio { get; set; } = null!;
    public string? Gender { get; set; } = null!;
    public string? Username { get; set; } = null!;
    public IFormFile? Avatar { get; set; } = null!;
    public IFormFile? Background { get; set; } = null!;
}
