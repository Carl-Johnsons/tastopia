namespace UserService.Domain.Responses;
public class AdminUserReportResponse
{
    public Guid ReportId { get; set; }
    public Guid ReportedId { get; set; }
    public string ReportedUsername { get; set; } = null!;
    public string ReportedDisplayName { get; set; } = null!;
    public Guid ReporterAccountId { get; set; }
    public string ReporterDisplayName { get; set; } = null!;
    public string ReportReason { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = null!;
}
