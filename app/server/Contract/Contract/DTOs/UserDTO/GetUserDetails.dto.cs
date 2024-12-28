namespace Contract.DTOs.UserDTO;
public class GetUserDetailsDTO
{
    public Guid UserId { get; set; }
    public UserDTO User { get; set; } = null!;
    public AccountDTO Account { get; set; } = null!;
}

public class UserDTO
{
    public string DisplayName { get; set; } = null!;

    public string AvatarUrl { get; set; } = null!;

    public string BackgroundUrl { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string? Gender { get; set; }

    public string? Bio { get; set; }

    public string? Address { get; set; }

    public virtual int? TotalFollwer { get; set; }

    public virtual int? TotalFollowing { get; set; }

    public virtual int? TotalRecipe { get; set; }
}

public class AccountDTO
{
    public bool IsActive { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set;}

    public string? PhoneNumber { get; set; }

}
