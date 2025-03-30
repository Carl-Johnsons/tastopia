using Contract.Constants;
using Contract.Utilities;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Errors;
using UserService.Domain.Responses;
namespace UserService.Application.UserReports.Queries;
public record GetUserReportDetailByAccountIdQuery : IRequest<Result<PaginatedAdminUserReportDetailListResponse?>>
{
    public string Lang { get; init; } = "en";
    public Guid AccountId { get; init; }
    public int? Skip { get; init; }
}

public class GetUserReportDetailByAccountIdQueryHandler : IRequestHandler<GetUserReportDetailByAccountIdQuery, Result<PaginatedAdminUserReportDetailListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<AdminUserReportDetailResponse, AdvancePaginatedMetadata> _paginateDataUtility;

    public GetUserReportDetailByAccountIdQueryHandler(IApplicationDbContext context, IPaginateDataUtility<AdminUserReportDetailResponse, AdvancePaginatedMetadata> paginateDataUtility)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
    }

    public async Task<Result<PaginatedAdminUserReportDetailListResponse?>> Handle(GetUserReportDetailByAccountIdQuery request, CancellationToken cancellationToken)
    {
        var normalizedLangue = LanguageUtility.ToIso6391(request.Lang);
        var accountId = request.AccountId;
        var skip = request.Skip;
        if (accountId == Guid.Empty || skip == null)
        {
            return Result<PaginatedAdminUserReportDetailListResponse?>.Failure(UserReportError.NullParameter, "AccountId or Skip is null");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.AccountId == accountId);

        if (user == null)
        {
            return Result<PaginatedAdminUserReportDetailListResponse?>.Failure(UserError.NotFound, "Not found user.");
        }

        var resultQuery = _context.UserReports.Include(rp => rp.Reporter).Where(rp => rp.ReportedId == user.AccountId).AsEnumerable().Select(rp => new AdminUserReportDetailResponse
        {
            ReportId = rp.Id,
            ReportedId = rp.ReportedId,
            ReporterId = rp.ReporterId,
            ReportedUsername = user.AccountUsername,
            ReporterUsername = rp.Reporter!.AccountUsername,
            ReportedDisplayName = user.DisplayName,
            ReportedIsActive = user.IsAccountActive,
            ReportedAvtUrl = user.AvatarUrl,
            ReporterAvtUrl = rp.Reporter!.AvatarUrl,
            Status = rp.Status.ToString(),
            ReportReason = ReportReasonData.ReportUserReasons.Where(rs => rp.ReasonCodes.Contains(rs.Code)).Select(rs => normalizedLangue == LanguageValidation.Vi ? rs.Vi : rs.En).ToList(),
            AdditionalDetails = rp.AdditionalDetails,
            CreatedAt = rp.CreatedAt
        }).AsQueryable();
        

        var totalRow =  resultQuery.Count();
        var totalPage = (totalRow + USER_CONSTANTS.ADMIN_USER_REPORT_LIMIT - 1) / USER_CONSTANTS.ADMIN_USER_REPORT_LIMIT;

        resultQuery = _paginateDataUtility.PaginateQuery(resultQuery, new PaginateParam
        {
            Limit = USER_CONSTANTS.ADMIN_USER_REPORT_LIMIT,
            Offset = (skip ?? 0) * USER_CONSTANTS.ADMIN_USER_REPORT_LIMIT,
            SortBy = "CreatedAt",
            SortOrder = SortType.DESC
        });

        var result = resultQuery.ToList();

        if(result == null || result.Count == 0)
        {
            return Result<PaginatedAdminUserReportDetailListResponse?>.Success(new PaginatedAdminUserReportDetailListResponse
            {
                PaginatedData = [],
                Metadata = new AdvancePaginatedMetadata
                {
                    HasNextPage = false,
                    TotalPage = 0,
                }
            });
        }

        var hasNextPage = true;

        if (skip >= totalPage - 1)
        {
            hasNextPage = false;
        }
        var adminGetUserReportDetailResponse = new PaginatedAdminUserReportDetailListResponse
        {
            PaginatedData = result,
            Metadata = new AdvancePaginatedMetadata
            {
                HasNextPage = hasNextPage,
                TotalPage = totalPage
            }
        };
        return Result<PaginatedAdminUserReportDetailListResponse?>.Success(adminGetUserReportDetailResponse);
    }
}
