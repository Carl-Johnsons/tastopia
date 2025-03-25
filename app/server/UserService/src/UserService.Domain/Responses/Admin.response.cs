namespace UserService.Domain.Responses;

public class AdminResponse
{
    public Guid AccountId { get; set; }
    public string UserName { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null!;
    public DateTime? Dob { get; set; }
    public bool IsActive { get; set; }
    public string? Address { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
