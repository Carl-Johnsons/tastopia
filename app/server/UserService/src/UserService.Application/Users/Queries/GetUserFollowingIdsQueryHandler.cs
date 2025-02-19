using Microsoft.EntityFrameworkCore;
using UserService.Domain.Errors;
namespace UserService.Application.Users.Queries;
public class GetUserFollowingIdsQuery : IRequest<Result<List<Guid>?>>
{
    public Guid? AccountId { get; set; }
}

public class GetUserFollowingIdsQueryHandler : IRequestHandler<GetUserFollowingIdsQuery, Result<List<Guid>?>>
{
    private readonly IApplicationDbContext _context;

    public GetUserFollowingIdsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<List<Guid>?>> Handle(GetUserFollowingIdsQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        if (accountId == null || accountId == Guid.Empty)
        {
            return Result<List<Guid>?>.Failure(UserError.NullParameters, "accountId is null!");
        }
        var ids = await _context.UserFollows.Where(f => f.FollowerId == accountId).Select(f => f.FollowingId).ToListAsync();
        return Result<List<Guid>?>.Success(ids);
    }
}
