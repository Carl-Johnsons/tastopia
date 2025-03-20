namespace UserService.Domain.Responses;
public class AdminUserReportDetailResponse
{
    public Guid ReportId { get; set; }
    public Guid ReportedId { get; set; }
    public string ReportedUsername { get; set; } = null!;
    public string ReportedDisplayName { get; set; } = null!;
    public string ReportedAvtUrl { get; set; } = null!;
    public List<string> ReportReason { get; set; } = [];
    public string? AdditionalDetails { get; set; } 
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = null!;
}
