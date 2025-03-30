using AccountProto;
using Contract.DTOs;
using UserService.Domain.Responses;

namespace UserService.Application.Users.Queries;

public record GetAdminsQuery : IRequest<Result<PaginatedAdminListResponse>>
{
    public PaginatedDTO DTO { get; set; } = null!;
}


public class GetAdminsQueryHandler : IRequestHandler<GetAdminsQuery, Result<PaginatedAdminListResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<AdminResponse, NumberedPaginatedMetadata> _paginateDataUtility;
    private readonly GrpcAccount.GrpcAccountClient _grpcAccountClient;
    public GetAdminsQueryHandler(IApplicationDbContext context,
                                 IPaginateDataUtility<AdminResponse, NumberedPaginatedMetadata> paginateDataUtility,
                                 GrpcAccount.GrpcAccountClient grpcAccountClient)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _grpcAccountClient = grpcAccountClient;
    }

    public async Task<Result<PaginatedAdminListResponse>> Handle(GetAdminsQuery request,
                                                           CancellationToken cancellationToken)
    {
        var paginatedDTO = request.DTO;
        var limit = request.DTO.Limit ?? USER_CONSTANTS.ADMIN_USER_LIMIT;
        var offset = request.DTO.Skip * limit ?? 0;
        var adminRoleAccountQuery = _context.Users.Where(u => u.IsAdmin);

        if (!string.IsNullOrEmpty(paginatedDTO.Keyword))
        {
            var keyword = paginatedDTO.Keyword.ToLower();

            var searchAccountResponse = await _grpcAccountClient.SearchAccountAsync(new GrpcSearchAccountRequest
            {
                Keyword = keyword,
            }, cancellationToken: cancellationToken);

            var searchAuthorIds = searchAccountResponse.AccountIds.ToHashSet();

            adminRoleAccountQuery = adminRoleAccountQuery.Where(u => u.AccountUsername.ToLower().Contains(keyword) ||
                                                                u.DisplayName.ToLower().Contains(keyword) ||
                                                                (u.Address != null && u.Address.ToLower().Contains(keyword)) ||
                                                                searchAuthorIds.Contains(u.AccountId.ToString())
            );
        }

        var adminRoleAccounts = adminRoleAccountQuery.ToList();
        var accIds = adminRoleAccounts.Select(a => a.AccountId.ToString()).ToList();
        if (accIds == null || accIds.Count == 0)
        {
            return Result<PaginatedAdminListResponse>.Success(new PaginatedAdminListResponse
            {

                PaginatedData = [],
                Metadata = new NumberedPaginatedMetadata
                {
                    CurrentPage = (paginatedDTO.Skip ?? 0) + 1,
                    TotalPage = 0,
                    TotalRow = 0
                }
            });
        }

        var mapAccount = await _grpcAccountClient.GetAdminAccountDetailAsync(new GrpcAccountIdListRequest
        {
            AccountIds = { accIds }
        }, cancellationToken: cancellationToken);

        // Exclude all all role admin only get the role "ADMIN"
        var adminQuery = adminRoleAccounts.Where(acc => mapAccount.Accounts.ContainsKey(acc.AccountId.ToString()));

        var totalRow = adminQuery.Count();
        var totalPage = (totalRow + limit - 1) / limit;

        var query = adminQuery
            .Select(acc => new AdminResponse
            {
                AccountId = acc.AccountId,
                DisplayName = acc.DisplayName,
                UserName = acc.AccountUsername,
                Dob = acc.Dob,
                IsActive = acc.IsAccountActive,
                Email = mapAccount.Accounts[acc.AccountId.ToString()].Email,
                PhoneNumber = mapAccount.Accounts[acc.AccountId.ToString()].PhoneNumber,
                Address = acc.Address,
                CreatedAt = mapAccount.Accounts[acc.AccountId.ToString()].CreatedAt.ToDateTime(),
                UpdatedAt = mapAccount.Accounts[acc.AccountId.ToString()].UpdatedAt.ToDateTime()
            })
            .AsQueryable();

        var paginatedList = _paginateDataUtility.PaginateQuery(query, new PaginateParam
        {
            Limit = limit,
            Offset = offset,
            SortBy = paginatedDTO.SortBy ?? "CreatedAt",
            SortOrder = paginatedDTO.SortOrder ?? SortType.DESC
        }).ToList();

        var paginatedResponse = new PaginatedAdminListResponse
        {

            PaginatedData = paginatedList,
            Metadata = new NumberedPaginatedMetadata
            {
                CurrentPage = (paginatedDTO.Skip ?? 0) + 1,
                TotalPage = totalPage,
                TotalRow = totalRow
            }
        };
        return Result<PaginatedAdminListResponse>.Success(paginatedResponse);
    }
}
