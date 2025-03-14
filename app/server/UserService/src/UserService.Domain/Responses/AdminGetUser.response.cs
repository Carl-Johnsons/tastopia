namespace UserService.Domain.Responses;

public class AdminGetUserResponse
{
    public Guid AccountId { get; set; }
    public string DisplayName { get; set; } = null!;
    public DateTime? Dob { get; set; }
    public string? Address { get; set; }
    public bool IsAccountActive { get; set; } = true;
    public string AccountUsername { get; set; } = null!;
    //Account
    public string? AccountEmail { get; set; }
    public string? AccountPhoneNumber { get; set; }

}
