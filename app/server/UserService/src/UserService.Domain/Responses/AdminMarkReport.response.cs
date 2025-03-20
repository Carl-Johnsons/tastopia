using UserService.Domain.Entities;
namespace UserService.Domain.Responses;
public class AdminMarkReportResponse
{
    public UserReport UserReport { get; set; } = null!;
    public bool IsReopened { get; set; } = false;
}
