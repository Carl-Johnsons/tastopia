using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Errors;
using UserService.Domain.Responses;

namespace UserService.Application.Users.Queries;

public class GetUserFollowersQuery : IRequest<Result<PaginatedSimpleUserListResponse?>>
{
    public string Keyword { get; set; } = null!;
    public int? Skip { get; set; } = null!;
    public Guid? AccountId { get; set; }
}

public class GetUserFollowersQueryHandler : IRequestHandler<GetUserFollowersQuery, Result<PaginatedSimpleUserListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<User, AdvancePaginatedMetadata> _paginateDataUtility;


    public GetUserFollowersQueryHandler(IApplicationDbContext context, IPaginateDataUtility<User, AdvancePaginatedMetadata> paginateDataUtility)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
    }

    public async Task<Result<PaginatedSimpleUserListResponse?>> Handle(GetUserFollowersQuery request, CancellationToken cancellationToken)
    {
        var keyword = request.Keyword;
        var skip = request.Skip;
        var accountId = request.AccountId;

        if (skip == null || accountId == null || accountId == Guid.Empty)
        {
            return Result<PaginatedSimpleUserListResponse?>.Failure(UserError.NullParameters, "skip or accountId is null!");
        }
        var userQuery = _context.UserFollows.Where(f => f.FollowingId == accountId).Join(_context.Users,
                follow => follow.FollowerId,
                user => user.AccountId,
                (follow, user) => user
        ).AsQueryable();

        if(!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            userQuery = userQuery.Where(u => u.IsAccountActive && !u.IsAdmin &&
                                        (u.DisplayName.ToLower().Contains(keyword) ||
                                         u.AccountUsername.ToLower().Contains(keyword)
                                        ));
        }

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
