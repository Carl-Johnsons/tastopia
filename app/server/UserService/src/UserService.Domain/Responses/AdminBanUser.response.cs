using UserService.Domain.Entities;
namespace UserService.Domain.Responses;
public class AdminBanUserResponse
{
    public Guid AdminId { get; set; }
    public Guid UserId { get; set; }
    public bool IsRestored { get; set; } = false;
    public User User { get; set; } = null!;
}
