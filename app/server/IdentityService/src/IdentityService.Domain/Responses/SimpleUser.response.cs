namespace IdentityService.Domain.Responses;

public class SimpleUserResponse
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; } = null!;
    public string AvtUrl { get; set; } = null!;

}
