using Contract.Constants;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Application.Account.Queries;

public record GetAdminAccountDetailQuery : IRequest<Result<List<ApplicationAccount>?>>
{
    public HashSet<Guid> Ids { get; set; } = null!;
}

public class GetAdminAccountDetailQueryHandler : IRequestHandler<GetAdminAccountDetailQuery, Result<List<ApplicationAccount>?>>
{
    private readonly UserManager<ApplicationAccount> _userManager;

    public GetAdminAccountDetailQueryHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<List<ApplicationAccount>?>> Handle(GetAdminAccountDetailQuery request, CancellationToken cancellationToken)
    {
        var ids = request.Ids;

        if (ids == null || !ids.Any())
        {
            return Result<List<ApplicationAccount>>.Failure(AccountError.NotFound);
        }
        var idStrings = ids.Select(id => id.ToString()).ToHashSet();

        var adminAcc = (List<ApplicationAccount>)await _userManager.GetUsersInRoleAsync(Roles.Code.ADMIN.ToString());

        return Result<List<ApplicationAccount>?>.Success(adminAcc);
    }
}
