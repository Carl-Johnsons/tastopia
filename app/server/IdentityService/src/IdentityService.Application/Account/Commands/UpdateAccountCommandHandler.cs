using Microsoft.AspNetCore.Identity;

namespace IdentityService.Application.Account.Commands;

public class UpdateAccountCommand : IRequest<Result>
{
    public Guid AccountId { get; init; }
    public string? UserName { get; init; } = null!;
}


public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;

    public UpdateAccountCommandHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var accounts = _userManager.Users.Where(acc => acc.Id == request.AccountId.ToString() 
                                                 || acc.UserName == request.UserName).ToList();

        var account = accounts.SingleOrDefault(acc => acc.Id == request.AccountId.ToString());

        if (account == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (!string.IsNullOrEmpty(request.UserName))
        {
            var isExistedUsername = accounts.Any(a => a.Id != request.AccountId.ToString() && a.UserName == request.UserName);
            if (isExistedUsername)
            {
                return Result.Failure(AccountError.UsernameAlreadyExisted);
            }
            account.UserName = request.UserName;
        }

        var result = await _userManager.UpdateAsync(account);

        if (!result.Succeeded)
        {
            return Result.Failure(AccountError.UpdateAccountFailed);
        }

        return Result.Success();
    }
}
