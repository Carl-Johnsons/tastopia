using Microsoft.EntityFrameworkCore;
using UserService.Domain.Errors;
namespace UserService.Application.Users.Queries;
public class GetUserFollowerIdsQuery : IRequest<Result<List<Guid>?>>
{
    public Guid? AccountId { get; set; }
}

public class GetUserFollowerIdsQueryHandler : IRequestHandler<GetUserFollowerIdsQuery, Result<List<Guid>?>>
{
    private readonly IApplicationDbContext _context;

    public GetUserFollowerIdsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Guid>?>> Handle(GetUserFollowerIdsQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        if (accountId == null || accountId == Guid.Empty)
        {
            return Result<List<Guid>?>.Failure(UserError.NullParameters, "accountId is null!");
        }
        var ids = await _context.UserFollows.Where(f => f.FollowingId == accountId).Select(f => f.FollowerId).ToListAsync();
        return Result<List<Guid>?>.Success(ids);
    }
}
