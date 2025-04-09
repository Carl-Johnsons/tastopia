
using Contract.Constants;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Account.Commands;

public record ChangePasswordFirstTimeLoginCommand : IRequest<Result>
{
    public string Identifier { get; set; } = null!;
    public string Password { get; set; } = null!;
}


public class ChangePasswordFirstTimeLoginCommandHandler : IRequestHandler<ChangePasswordFirstTimeLoginCommand, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;

    public ChangePasswordFirstTimeLoginCommandHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ChangePasswordFirstTimeLoginCommand request,
                               CancellationToken cancellationToken)
    {
        ApplicationAccount? account = null;
        var method = IdentifierUtility.Check(request.Identifier);
        switch (method)
        {
            case AccountMethod.Email:
                account = await _userManager.Users.SingleOrDefaultAsync(a => (a.Email ?? "").ToLower() == request.Identifier.ToLower());
                break;
            case AccountMethod.Phone:
                account = await _userManager.Users.SingleOrDefaultAsync(a => a.PhoneNumber == request.Identifier);
                break;
            case AccountMethod.Username:
                account = await _userManager.Users.SingleOrDefaultAsync(a => (a.UserName ?? "").ToLower() == request.Identifier.ToLower());
                break;
            default:
                return Result.Failure(AccountError.InvalidAccountMethod);
        }

        if (account == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        account.IsFirstTimeLogin = false;
        account.PasswordHash = _userManager.PasswordHasher.HashPassword(account, request.Password);
        var res = await _userManager.UpdateAsync(account);
        if (!res.Succeeded)
        {
            return Result.Failure(AccountError.ResetPasswordFailed);
        }

        return Result.Success();
    }
}
