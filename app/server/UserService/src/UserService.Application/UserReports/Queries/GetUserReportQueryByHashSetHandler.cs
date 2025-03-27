using Contract.Constants;
using Contract.Utilities;
using UserService.Domain.Responses;

namespace UserService.Application.UserReports.Queries;
public record GetUserReportQueryByHashSet : IRequest<Result<Dictionary<Guid, AdminUserReportResponse>?>>
{
    public string Lang { get; init; } = "en";
    public HashSet<Guid> ReportIds { get; init; } = null!;
}

public class GetUserReportQueryByHashSetHandler : IRequestHandler<GetUserReportQueryByHashSet, Result<Dictionary<Guid, AdminUserReportResponse>?>>
{
    private readonly IApplicationDbContext _context;

    public GetUserReportQueryByHashSetHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result<Dictionary<Guid, AdminUserReportResponse>?>> Handle(GetUserReportQueryByHashSet request, CancellationToken cancellationToken)
    {
        var normalizedLangue = LanguageUtility.ToIso6391(request.Lang);
        var reportedIds = _context.UserReports.Select(rp => rp.ReportedId);
        var reporterIds = _context.UserReports.Select(rp => rp.ReporterId);
        var userIds = reportedIds.Union(reporterIds).ToHashSet();
        var userDict = _context.Users.Where(u => userIds.Contains(u.AccountId)).ToDictionary(u => u.AccountId);
        var reportDictionary = _context.UserReports.Where(rp => request.ReportIds.Contains(rp.Id)).AsEnumerable().Select(rp => new AdminUserReportResponse
        {
            ReportId = rp.Id,
            ReportedId = rp.ReportedId,
            ReportedUsername = userDict[rp.ReportedId].AccountUsername,
            ReportedDisplayName = userDict[rp.ReportedId].DisplayName,
            ReportedIsActive = userDict[rp.ReportedId].IsAccountActive,
            ReporterAccountId = rp.ReporterId,
            ReporterDisplayName = userDict[rp.ReporterId].DisplayName,
            Status = rp.Status.ToString(),
            ReportReason = string.Join(", ", ReportReasonData.ReportUserReasons.Where(rs => rp.ReasonCodes.Contains(rs.Code)).Select(rs => normalizedLangue == LanguageValidation.Vi ? rs.Vi : rs.En)),
            CreatedAt = rp.CreatedAt
        }).ToDictionary(rp => rp.ReportId, rp => rp);

        return Task.FromResult(Result<Dictionary<Guid, AdminUserReportResponse>?>.Success(reportDictionary));
    }
}
