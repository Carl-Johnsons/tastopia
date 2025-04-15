using Contract.Constants;
using Microsoft.AspNetCore.Identity;
namespace IdentityService.Application.Account.Commands;

public record VerifyUpdateIdentifierCommand : IRequest<Result>
{
    public Guid AccountId { get; set; }
    public string Identifier { get; set; } = null!;
    public string OTP { get; set; } = null!;
    public AccountMethod Method { get; set; }
}


public class VerifyUpdateIdentifierCommandHandler : IRequestHandler<VerifyUpdateIdentifierCommand, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;

    public VerifyUpdateIdentifierCommandHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(VerifyUpdateIdentifierCommand request, CancellationToken cancellationToken)
    {
        if (request.Method == AccountMethod.Email) return await VerifyUpdateEmail(request, cancellationToken);
        if (request.Method == AccountMethod.Phone) return await VerifyUpdatePhone(request, cancellationToken);
        return Result.Failure(AccountError.InvalidAccountMethod, "Wrong account method");
    }

    private async Task<Result> VerifyUpdateEmail(VerifyUpdateIdentifierCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.AccountId.ToString());
        if (user == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (user.EmailOTP != request.OTP)
        {
            return Result.Failure(AccountError.InvalidOTP);
        }

        // The expiry store in database in UTC time but when it return value it return local time
        if (user.EmailOTPExpiry < DateTime.Now)
        {
            return Result.Failure(AccountError.OTPExpired);
        }
        user.Email = request.Identifier;
        user.EmailConfirmed = true;
        user.EmailOTP = null;
        user.EmailOTPCreated = null;
        user.EmailOTPExpiry = null;
        user.RequestOTPCount = 0;
        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            return Result.Failure(AccountError.VerifyFailed);
        }

        return Result.Success();
    }
    private async Task<Result> VerifyUpdatePhone(VerifyUpdateIdentifierCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.AccountId.ToString());
        if (user == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (user.PhoneOTP != request.OTP)
        {
            return Result.Failure(AccountError.InvalidOTP);
        }

        if (user.EmailOTPExpiry < DateTime.Now)
        {
            return Result.Failure(AccountError.OTPExpired);
        }
        user.PhoneNumber = request.Identifier;
        user.PhoneNumberConfirmed = true;
        user.PhoneOTP = null;
        user.PhoneOTPCreated = null;
        user.PhoneOTPExpiry = null;
        user.RequestOTPCount = 0;
        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            return Result.Failure(AccountError.VerifyFailed);
        }

        return Result.Success();
    }
}
