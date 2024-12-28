using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entities;
using UserService.Domain.Errors;
using UserService.Domain.Responses;

namespace UserService.Application.Users.Commands;

public class SearchUsersCommand : IRequest<Result<PaginatedSearchUserListResponse?>>
{
    [Required]
    public int? Skip { get; init; }

    [Required]
    public string? Keyword { get; init; }

    [Required]
    public Guid? UserId { get; init; }
}

public class SearchUsersCommandHandler : IRequestHandler<SearchUsersCommand, Result<PaginatedSearchUserListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IServiceBus _serviceBus;
    private readonly IPaginateDataUtility<User, AdvancePaginatedMetadata> _paginateDataUtility;

    public SearchUsersCommandHandler(IApplicationDbContext context, IServiceBus serviceBus, IPaginateDataUtility<User, AdvancePaginatedMetadata> paginateDataUtility)
    {
        _context = context;
        _serviceBus = serviceBus;
        _paginateDataUtility = paginateDataUtility;
    }

    public async Task<Result<PaginatedSearchUserListResponse?>> Handle(SearchUsersCommand request, CancellationToken cancellationToken)
    {
        var skip = request.Skip;
        var keyword = request.Keyword;
        var userId = request.UserId;

        if(skip == null || string.IsNullOrEmpty(keyword) || userId == null)
        {
            return Result<PaginatedSearchUserListResponse?>.Failure(UserError.NotFound);
        }

        var userQuery = _context.Users.OrderByDescending(u => u.DisplayName).AsQueryable();

        userQuery = userQuery.Where(u => u.DisplayName.ToLower().Contains(keyword.ToLower()));

        var totalPage = (await userQuery.CountAsync() + USER_CONSTANTS.USER_LIMIT - 1) / USER_CONSTANTS.USER_LIMIT;

        userQuery = _paginateDataUtility.PaginateQuery(userQuery, new PaginateParam
        {
            Offset = skip ?? 0 * USER_CONSTANTS.USER_LIMIT,
            Limit = USER_CONSTANTS.USER_LIMIT
        });

        var isFollowingMap = new Dictionary<Guid, bool>();

        var currentUserFollowings = await _context.UserFollows.Where(uf => uf.FollowerId == userId).ToDictionaryAsync(uf => uf.FollowingId);

        var userList = await userQuery.Select(u => new SearchUserResponse
        {
            Id = u.Id,
            AvtUrl = u.AvatarUrl,
            Username = "",
            DisplayName = u.DisplayName,
            NumberOfRecipe = u.TotalRecipe ?? 0,
            IsFollowing = currentUserFollowings != null && currentUserFollowings.ContainsKey(u.Id),
        }).ToListAsync();

        if (userList == null || !userList.Any())
        {
            return Result<PaginatedSearchUserListResponse?>.Success(new PaginatedSearchUserListResponse
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

        var paginatedResponse = new PaginatedSearchUserListResponse
        {
            PaginatedData = userList,
            Metadata = new AdvancePaginatedMetadata
            {
                TotalPage = totalPage,
                HasNextPage = hasNextPage
            }
        };
        return Result<PaginatedSearchUserListResponse?>.Success(paginatedResponse);
    }
}
