namespace PostService.Domain.Common;

public class BasePaginatedDTO
{
    public int Skip { get; set; } = 0;
    public int Limit { get; set; } = 10;
}
