using Contract.Interfaces;

namespace Contract.DTOs;

public class PaginatedDTO
{
    public int? Skip { get; set; } = 0;
    public SortType? SortOrder {  get; set; } = SortType.DESC;
    public string? SortBy { get; set; }
    public string? Keyword { get; set; }
    public int? TotalRow { get; set; }
}
