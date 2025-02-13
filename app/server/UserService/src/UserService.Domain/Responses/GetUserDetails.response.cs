
namespace UserService.Domain.Responses;

public class GetUserDetailsResponse
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

    public bool IsFollowing { get; set; } = false;

    public bool IsCurrentUser { get; set; } = false;

    //Account
    public string? AccountEmail { get; set; }
    public string? AccountPhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
