using IdentityService.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Application.Account;

public record GetAccountDetailCommand : IRequest<Result<ApplicationAccount?>>
{
    public Guid Id { get; set; }
}


public class GetAccountDetailCommandHandler : IRequestHandler<GetAccountDetailCommand, Result<ApplicationAccount?>>
{
    private readonly UserManager<ApplicationAccount> _userManager;

    public GetAccountDetailCommandHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public Task<Result<ApplicationAccount?>> Handle(GetAccountDetailCommand request, CancellationToken cancellationToken)
    {
        var acc = _userManager.Users.SingleOrDefault(a => a.Id == request.Id.ToString());
        if (acc == null)
        {
            return Task.FromResult(Result<ApplicationAccount>.Failure(AccountError.NotFound));
        }
        return Task.FromResult(Result<ApplicationAccount?>.Success(acc));
    }
}
