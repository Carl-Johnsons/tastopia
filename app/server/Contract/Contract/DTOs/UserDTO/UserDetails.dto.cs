namespace Contract.DTOs.UserDTO;
public class UserDetailsDTO
{
    public Guid AccountId { get; set; }
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

    public bool IsAccountActive { get; set; } = true;

    public string AccountUsername { get; set; } = null!;

    public bool IsAdmin { get; set; } = false;

    public string? AccountEmail { get; set; }

    public string? AccountPhoneNumber { get; set; }
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

    public bool IsAccountActive { get; set; } = true;

    public string AccountUsername { get; set; } = null!;

    public bool IsAdmin { get; set; } = false;

}

public class AccountDTO
{
    public bool IsActive { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set;}

    public string? PhoneNumber { get; set; }

}
