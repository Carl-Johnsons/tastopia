namespace UserService.Domain.Responses;

public class AdminDetailResponse
{
    public Guid AccountId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public string Username { get; set; } = null!;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? Dob { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
