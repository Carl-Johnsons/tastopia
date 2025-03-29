using Contract.Constants;
using Contract.Utilities;
using UserService.Domain.Responses;

namespace UserService.Application.UserReports.Queries;
public record GetUserReportQueryByHashSet : IRequest<Result<Dictionary<Guid, AdminGrpcUserReportResponse>?>>
{
    public string Lang { get; init; } = "en";
    public HashSet<Guid> ReportIds { get; init; } = null!;
}

public class GetUserReportQueryByHashSetHandler : IRequestHandler<GetUserReportQueryByHashSet, Result<Dictionary<Guid, AdminGrpcUserReportResponse>?>>
{
    private readonly IApplicationDbContext _context;

    public GetUserReportQueryByHashSetHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result<Dictionary<Guid, AdminGrpcUserReportResponse>?>> Handle(GetUserReportQueryByHashSet request, CancellationToken cancellationToken)
    {
        var normalizedLangue = LanguageUtility.ToIso6391(request.Lang);
        var reportedIds = _context.UserReports.Select(rp => rp.ReportedId);
        var reporterIds = _context.UserReports.Select(rp => rp.ReporterId);
        var userIds = reportedIds.Union(reporterIds).ToHashSet();
        var userDict = _context.Users.Where(u => userIds.Contains(u.AccountId)).ToDictionary(u => u.AccountId);
        var reportDictionary = _context.UserReports.Where(rp => request.ReportIds.Contains(rp.Id)).AsEnumerable().Select(rp => new AdminGrpcUserReportResponse
        {
            Reporter = new Contract.DTOs.UserDTO.SimpleUser
            {
                AccountId = userDict[rp.ReporterId].AccountId,
                AccountUsername = userDict[rp.ReporterId].AccountUsername,
                AvtUrl = userDict[rp.ReporterId].AvatarUrl,
                DisplayName = userDict[rp.ReporterId].DisplayName
            },
            User = new Contract.DTOs.UserDTO.SimpleUser
            {
                AccountId = userDict[rp.ReportedId].AccountId,
                AccountUsername = userDict[rp.ReportedId].AccountUsername,
                AvtUrl = userDict[rp.ReportedId].AvatarUrl,
                DisplayName = userDict[rp.ReportedId].DisplayName
            },
            Report = new AdminGrpcReportResponse
            {
                Id = rp.Id,
                AdditionalDetail = rp.AdditionalDetails ?? "",
                Status = rp.Status.ToString(),
                Reasons = ReportReasonData.ReportUserReasons.Where(rs => rp.ReasonCodes.Contains(rs.Code)).Select(rs => normalizedLangue == LanguageValidation.Vi ? rs.Vi : rs.En).ToList(),
                CreatedAt = rp.CreatedAt,
                ReporterAccountId = rp.ReporterId
            }
        }).ToDictionary(rp => rp.Report.Id, rp => rp);

        return Task.FromResult(Result<Dictionary<Guid, AdminGrpcUserReportResponse>?>.Success(reportDictionary));
    }
}
