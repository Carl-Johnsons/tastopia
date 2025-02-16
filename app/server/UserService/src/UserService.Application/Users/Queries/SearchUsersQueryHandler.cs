using Contract.Event.TrackingEvent;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entities;
using UserService.Domain.Errors;
using UserService.Domain.Responses;

namespace UserService.Application.Users.Queries;

public class SearchUsersQuery : IRequest<Result<PaginatedSimpleUserListResponse?>>
{
    [Required]
    public int? Skip { get; init; }

    [Required]
    public string? Keyword { get; init; }

    [Required]
    public Guid? AccountId { get; init; }
}

public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, Result<PaginatedSimpleUserListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<User, AdvancePaginatedMetadata> _paginateDataUtility;
    private readonly IServiceBus _serviceBus;

    public SearchUsersQueryHandler(IApplicationDbContext context, IPaginateDataUtility<User, AdvancePaginatedMetadata> paginateDataUtility, IServiceBus serviceBus)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _serviceBus = serviceBus;
    }

    public async Task<Result<PaginatedSimpleUserListResponse?>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
        var skip = request.Skip;
        var keyword = request.Keyword;
        var accountId = request.AccountId;

        if (skip == null || keyword == null)
        {
            return Result<PaginatedSimpleUserListResponse?>.Failure(UserError.NotFound);
        }

        keyword = keyword.ToLower();

        if (keyword == "")
        {
            return Result<PaginatedSimpleUserListResponse?>.Success(new PaginatedSimpleUserListResponse
            {
                PaginatedData = [],
                Metadata = new AdvancePaginatedMetadata
                {
                    HasNextPage = false,
                    TotalPage = 0,
                }
            });
        }

        var userQuery = _context.Users.Where(u => u.AccountId != accountId).OrderByDescending(u => u.DisplayName).AsQueryable();

        userQuery = userQuery.Where(u => u.IsAccountActive && !u.IsAdmin &&
                                        (u.DisplayName.ToLower().Contains(keyword) ||
                                         u.AccountUsername.ToLower().Contains(keyword)
                                        ));

        var totalPage = (await userQuery.CountAsync() + USER_CONSTANTS.USER_LIMIT - 1) / USER_CONSTANTS.USER_LIMIT;

        userQuery = _paginateDataUtility.PaginateQuery(userQuery, new PaginateParam
        {
            Offset = (skip ?? 0) * USER_CONSTANTS.USER_LIMIT,
            Limit = USER_CONSTANTS.USER_LIMIT
        });

        var isFollowingMap = new Dictionary<Guid, bool>();

        var currentUserFollowings = await _context.UserFollows.Where(uf => uf.FollowerId == accountId).ToDictionaryAsync(uf => uf.FollowingId);

        var userList = await userQuery.Select(u => new SimpleUserResponse
        {
            Id = u.AccountId,
            AvtUrl = u.AvatarUrl,
            Username = u.AccountUsername,
            DisplayName = u.DisplayName,
            NumberOfRecipe = u.TotalRecipe ?? 0,
            IsFollowing = currentUserFollowings != null && currentUserFollowings.ContainsKey(u.AccountId),
        }).ToListAsync();

        if (userList == null || !userList.Any())
        {
            return Result<PaginatedSimpleUserListResponse?>.Success(new PaginatedSimpleUserListResponse
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

        var paginatedResponse = new PaginatedSimpleUserListResponse
        {
            PaginatedData = userList,
            Metadata = new AdvancePaginatedMetadata
            {
                TotalPage = totalPage,
                HasNextPage = hasNextPage
            }
        };
        return Result<PaginatedSimpleUserListResponse?>.Success(paginatedResponse);
    }
}
