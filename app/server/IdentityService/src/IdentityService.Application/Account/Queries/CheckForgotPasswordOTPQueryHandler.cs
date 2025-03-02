using Contract.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Account.Queries;

public record CheckForgotPasswordOTPQuery : IRequest<Result>
{
    public string Identifier { get; set; } = null!;
    public string OTP { get; set; } = null!;
    public AccountMethod Method { get; set; }
}


public class CheckForgotPasswordOTPQueryHandler : IRequestHandler<CheckForgotPasswordOTPQuery, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;

    public CheckForgotPasswordOTPQueryHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(CheckForgotPasswordOTPQuery request, CancellationToken cancellationToken)
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

        return Result.Success();
    }
}
