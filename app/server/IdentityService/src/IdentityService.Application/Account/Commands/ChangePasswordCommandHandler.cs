using Contract.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Account.Commands;

public record ChangePasswordCommand : IRequest<Result>
{
    public string Identifier { get; set; } = null!;
    public string OTP { get; set; } = null!;
    public string Password { get; set; } = null!;
    public AccountMethod Method { get; set; }
}

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    public UserManager<ApplicationAccount> _userManager { get; set; }

    public ChangePasswordCommandHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        ApplicationAccount? account = null;

        switch (request.Method)
        {
            case AccountMethod.Email:
                account = await _userManager.Users.SingleOrDefaultAsync(a => a.Email == request.Identifier);
                break;
            case AccountMethod.Phone:
                account = await _userManager.Users.SingleOrDefaultAsync(a => a.PhoneNumber == request.Identifier);
                break;
            default:
                return Result.Failure(AccountError.InvalidAccountMethod);
        }

        if (account == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (account.ForgotPasswordOTP != request.OTP)
        {
            return Result.Failure(AccountError.InvalidOTP);
        }

        if (DateTime.Now > account.ForgotPasswordExpiry)
        {
            return Result.Failure(AccountError.OTPExpired);
        }

        account.PasswordHash = _userManager.PasswordHasher.HashPassword(account, request.Password);
        account.ForgotPasswordOTP = null;
        account.ForgotPasswordExpiry = null;
        account.ForgotPasswordCreated = null;
        var res = await _userManager.UpdateAsync(account);
        if (!res.Succeeded)
        {
            return Result.Failure(AccountError.ResetPasswordFailed);
        }

        return Result.Success();
    }
}
