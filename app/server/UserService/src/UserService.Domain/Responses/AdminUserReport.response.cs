using Contract.DTOs.UserDTO;

namespace UserService.Domain.Responses;
public class AdminUserReportResponse
{
    public Guid ReportId { get; set; }
    public Guid ReportedId { get; set; }
    public string ReportedUsername { get; set; } = null!;
    public string ReportedDisplayName { get; set; } = null!;
    public bool ReportedIsActive { get; set; }
    public Guid ReporterAccountId { get; set; }
    public string ReporterDisplayName { get; set; } = null!;
    public string ReportReason { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = null!;
}

public class AdminGrpcUserReportResponse
{
    public SimpleUser User { get; set; } = null!;
    public SimpleUser Reporter { get; set; } = null!;
    public AdminGrpcReportResponse Report { get; set; } = null!;
}

public class AdminGrpcReportResponse
{
    public Guid Id { get; set; }
    public Guid ReporterAccountId { get; set; }
    public List<string> Reasons { get; set; } = [];
    public string? AdditionalDetail { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
