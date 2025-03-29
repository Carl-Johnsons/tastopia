
using UserService.Domain.Entities;

namespace UserService.Domain.Responses;

public class AdminGetUserDetailResponse
{
    public Guid AccountId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public DateTime? Dob { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public virtual int? TotalFollower { get; set; }
    public virtual int? TotalFollowing { get; set; }
    public virtual int? TotalRecipe { get; set; }
    public bool IsAccountActive { get; set; } = true;
    public string AccountUsername { get; set; } = null!;
    public string Role { get; set; } = null!;
    public bool IsCurrentUser { get; set; } = false;

    public List<UserSetting> Settings { get; set; } = []; 
    //Account
    public string? AccountEmail { get; set; }
    public string? AccountPhoneNumber { get; set; }

}
