namespace DuendeIdentityServer.DTOs;

public class BasePaginatedDTO
{
    public int Skip { get; set; } = 0;
    public int? Limit { get; set; } = null!;
}
