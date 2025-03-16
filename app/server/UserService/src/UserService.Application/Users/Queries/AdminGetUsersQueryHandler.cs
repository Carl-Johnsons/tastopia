using AccountProto;
using AutoMapper;
using Contract.DTOs;
using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entities;
using UserService.Domain.Errors;
using UserService.Domain.Responses;

namespace UserService.Application.Users.Queries;

public class AdminGetUsersQuery : IRequest<Result<PaginatedAdminGetUserListResponse?>>
{
    [Required]
    public Guid AccountId { get; init; }

    [Required]
    public PaginatedDTO PaginatedDTO { get; init; } = null!;
}

public class AdminGetUsersQueryHandler : IRequestHandler<AdminGetUsersQuery, Result<PaginatedAdminGetUserListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly GrpcAccount.GrpcAccountClient _grpcAccountClient;
    private readonly IPaginateDataUtility<User, NumberedPaginatedMetadata> _paginateDataUtility;


    public AdminGetUsersQueryHandler(IApplicationDbContext context,
        IMapper mapper,
        GrpcAccount.GrpcAccountClient grpcAccountClient,
        IPaginateDataUtility<User, NumberedPaginatedMetadata> paginateDataUtility)
    {
        _context = context;
        _mapper = mapper;
        _grpcAccountClient = grpcAccountClient;
        _paginateDataUtility = paginateDataUtility;
    }

    public async Task<Result<PaginatedAdminGetUserListResponse?>> Handle(AdminGetUsersQuery request, CancellationToken cancellationToken)
    {
        try {
            var accountId = request.AccountId;
            var paginatedDTO = request.PaginatedDTO;

            if (accountId == Guid.Empty || paginatedDTO.Skip == null)
            {
                return Result<PaginatedAdminGetUserListResponse?>.Failure(UserError.NullParameters, "Account or Skip Id is null");
            }

            var currentUser = await _context.Users
                .Where(user => user.AccountId == accountId)
                .FirstOrDefaultAsync();
            if (currentUser == null)
            {
                return Result<PaginatedAdminGetUserListResponse?>.Failure(UserError.NotFound);
            }
            if (!currentUser.IsAdmin)
            {
                return Result<PaginatedAdminGetUserListResponse?>.Failure(UserError.PermissionDenied);
            }

            var usersQuery = _context.Users.Where(u => !u.IsAdmin && u.AccountId != accountId).AsQueryable();
            if (!string.IsNullOrEmpty(paginatedDTO.Keyword))
            {
                var keyword = paginatedDTO.Keyword.ToLower();

                var searchAccountResponse = await _grpcAccountClient.SearchAccountAsync(new GrpcSearchAccountRequest
                {
                    Keyword = keyword,
                }, cancellationToken: cancellationToken);

                var searchAuthorIds = searchAccountResponse.AccountIds.ToHashSet();

                usersQuery = usersQuery.Where(u => u.AccountUsername.ToLower().Contains(keyword) ||
                                                   u.DisplayName.ToLower().Contains(keyword) ||
                                                   u.Address!.ToLower().Contains(keyword) ||
                                                   searchAuthorIds.Contains(u.AccountId.ToString())
                );
            }

            var limit = USER_CONSTANTS.ADMIN_USER_LIMIT;
            if(paginatedDTO.Limit != null)
            {
                limit = paginatedDTO.Limit.Value;
            }
            var totalRow = await usersQuery.CountAsync();
            var totalPage = (totalRow + limit - 1) / limit;

            usersQuery = _paginateDataUtility.PaginateQuery(usersQuery, new PaginateParam
            {
                Offset = (paginatedDTO.Skip ?? 0) * limit,
                Limit = limit,
                SortBy = paginatedDTO.SortBy != null ? paginatedDTO.SortBy : "AccountUsername",
                SortOrder = paginatedDTO.SortOrder
            });

            var userList = await usersQuery.Select(u => new AdminGetUserResponse
            {
                AccountId = u.AccountId,
                AccountUsername = u.AccountUsername,
                Address = u.Address,
                DisplayName = u.DisplayName,
                Dob = u.Dob,
                IsAccountActive = u.IsAccountActive,
            }).ToArrayAsync();

            if (userList == null || !userList.Any())
            {
                return Result<PaginatedAdminGetUserListResponse?>.Success(new PaginatedAdminGetUserListResponse
                {
                    PaginatedData = [],
                    Metadata = new NumberedPaginatedMetadata
                    {
                        CurrentPage = paginatedDTO.Skip!.Value,
                        TotalPage = 0,
                        TotalRow = totalRow,
                    }
                });
            }

            var accountIds = usersQuery
            .Select(u => u.AccountId)
            .Distinct()
            .ToHashSet();

            var response = await _grpcAccountClient.GetSimpleAccountsAsync(new GrpcAccountIdListRequest
            {
                AccountIds = { _mapper.Map<RepeatedField<string>>(accountIds) }
            }, cancellationToken: cancellationToken);

            if (response == null || response.Accounts.Count != accountIds.Count)
            {
                return Result<PaginatedAdminGetUserListResponse>.Failure(UserError.NotFound);
            }

            foreach (var u in userList)
            {
                u.AccountEmail = response.Accounts[u.AccountId.ToString()].Email;
                u.AccountPhoneNumber = response.Accounts[u.AccountId.ToString()].PhoneNumber;
            }

            var paginatedResponse = new PaginatedAdminGetUserListResponse
            {
                Metadata = new NumberedPaginatedMetadata
                {
                    CurrentPage = (paginatedDTO.Skip ?? 0) + 1,
                    TotalPage = totalPage,
                    TotalRow = totalRow,
                },
                PaginatedData = userList,
            };

            return Result<PaginatedAdminGetUserListResponse?>.Success(paginatedResponse);
        }
        catch(Exception ex)
        {
            return Result<PaginatedAdminGetUserListResponse?>.Failure(UserError.UpdateUserFail, ex.Message);

        }

    }
}
