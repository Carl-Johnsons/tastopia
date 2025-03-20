using Contract.Constants;
using Contract.DTOs;
using Contract.Utilities;
using MongoDB.Driver;
using UserService.Domain.Responses;

namespace UserService.Application.UserReports.Queries;
public record GetUserReportsQuery : IRequest<Result<PaginatedAdminUserReportListResponse>>
{
    public string Lang { get; init; } = "en";
    public PaginatedDTO? paginatedDTO { get; init; } = null!;
}

public class GetUserReportsQueryHandler : IRequestHandler<GetUserReportsQuery, Result<PaginatedAdminUserReportListResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<AdminUserReportResponse, NumberedPaginatedMetadata> _paginateDataUtility;

    public GetUserReportsQueryHandler(IApplicationDbContext context,
                                       IPaginateDataUtility<AdminUserReportResponse, NumberedPaginatedMetadata> paginateDataUtility)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
    }

    public async Task<Result<PaginatedAdminUserReportListResponse>> Handle(GetUserReportsQuery request, CancellationToken cancellationToken)
    {
        var keyword = request.paginatedDTO?.Keyword;

        var normalizedLangue = LanguageUtility.ToIso6391(request.Lang);
        var reportedIds = _context.UserReports.Select(rp => rp.ReportedId);
        var reporterIds = _context.UserReports.Select(rp => rp.ReporterId);
        var userIds = reportedIds.Union(reporterIds).ToHashSet();
        var userDict = _context.Users.Where(u => userIds.Contains(u.AccountId)).ToDictionary(u => u.AccountId);
        var resultQuery = _context.UserReports.AsEnumerable().Select(rp => new AdminUserReportResponse
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
        }).AsQueryable();
        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            resultQuery = resultQuery.Where(urr =>
                urr.ReportedDisplayName.ToLower().Contains(keyword) ||
                urr.ReportedUsername.ToLower().Contains(keyword) ||
                urr.ReporterDisplayName.ToLower().Contains(keyword) ||
                urr.ReportReason.ToLower().Contains(keyword)
                ).AsQueryable();
        }

        var limit = request.paginatedDTO?.Limit ?? USER_CONSTANTS.ADMIN_USER_REPORT_LIMIT;
        var totalRow =  resultQuery.Count();
        var totalPage = (totalRow + limit - 1) / limit;

        resultQuery = _paginateDataUtility.PaginateQuery(resultQuery, new PaginateParam
        {
            Limit = limit,
            Offset = (request.paginatedDTO?.Skip ?? 0) * limit,
            SortBy = request.paginatedDTO?.SortBy ?? "CreatedAt",
            SortOrder = request.paginatedDTO?.SortOrder ?? SortType.DESC
        });

        var result = resultQuery.ToList();

        var adminGetUserReportResponse = new PaginatedAdminUserReportListResponse
        {
            PaginatedData = result,
            Metadata = new NumberedPaginatedMetadata
            {
                CurrentPage = (request.paginatedDTO?.Skip ?? 0) + 1,
                TotalPage = totalPage,
                TotalRow = totalRow
            }
        };

        return Result<PaginatedAdminUserReportListResponse>.Success(adminGetUserReportResponse);
    }
}
