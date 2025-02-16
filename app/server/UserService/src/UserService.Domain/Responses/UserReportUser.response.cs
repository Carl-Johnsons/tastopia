using UserService.Domain.Entities;
namespace UserService.Domain.Responses;

public class UserReportUserResponse
{
    public UserReport Report { get; set; } = null!;
    public bool IsRemoved { get; set; }
}
