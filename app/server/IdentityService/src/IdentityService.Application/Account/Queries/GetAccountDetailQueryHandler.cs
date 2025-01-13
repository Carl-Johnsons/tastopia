using IdentityService.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Account.Queries;

public record GetAccountDetailQuery : IRequest<Result<List<ApplicationAccount>?>>
{
    public HashSet<Guid> Ids { get; set; } = null!;
}


public class GetAccountDetailQueryHandler : IRequestHandler<GetAccountDetailQuery, Result<List<ApplicationAccount>?>>
{
    private readonly UserManager<ApplicationAccount> _userManager;

    public GetAccountDetailQueryHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<List<ApplicationAccount>?>> Handle(GetAccountDetailQuery request, CancellationToken cancellationToken)
    {
        var ids = request.Ids;

        if (ids == null || !ids.Any())
        {
            return Result<List<ApplicationAccount>>.Failure(AccountError.NotFound);
        }
        var idStrings = ids.Select(id => id.ToString()).ToHashSet();

        var acc = await _userManager.Users.Where(a => idStrings.Contains(a.Id)).ToListAsync();

        if (acc == null || !acc.Any())
        {
            return Result<List<ApplicationAccount>>.Failure(AccountError.NotFound);
        }

        return Result<List<ApplicationAccount>?>.Success(acc);
    }
}
