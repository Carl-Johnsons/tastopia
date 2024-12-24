using Contract.Constants;
using IdentityService.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace IdentityService.Application.Account;

public record VerifyAccountCommand : IRequest<Result>
{
    public Guid AccountId { get; set; }
    public string OTP { get; set; } = null!;
    public AccountMethod Method { get; set; }
}


public class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;

    public VerifyAccountCommandHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine(JsonConvert.SerializeObject(request));
        switch (request.Method)
        {
            case AccountMethod.Email:
                return await VerifyEmail(request, cancellationToken);
            case AccountMethod.Phone:
                return await VerifyPhone(request, cancellationToken);
            default:
                return Result.Failure(AccountError.VerifyFailed);
        }
    }

    public async Task<Result> VerifyEmail(VerifyAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.AccountId.ToString());
        if (user == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (user.EmailConfirmed)
        {
            return Result.Failure(AccountError.EmailAlreadyConfirmed);
        }

        if (user.EmailOTP != request.OTP)
        {
            return Result.Failure(AccountError.InvalidOTP);
        }

        Console.WriteLine("Now:" + DateTime.Now);
        Console.WriteLine("Expired:" + user.EmailOTPExpiry.ToString());

        // The expiry store in database in UTC time but when it return value it return local time
        if (user.EmailOTPExpiry < DateTime.Now)
        {
            return Result.Failure(AccountError.OTPExpired);
        }

        user.EmailConfirmed = true;
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

    public async Task<Result> VerifyPhone(VerifyAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.AccountId.ToString());
        if (user == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (user.PhoneNumberConfirmed)
        {
            return Result.Failure(AccountError.PhoneAlreadyConfirmed);
        }

        if (user.PhoneOTP != request.OTP)
        {
            return Result.Failure(AccountError.InvalidOTP);
        }

        if (user.EmailOTPExpiry < DateTime.Now)
        {
            return Result.Failure(AccountError.OTPExpired);
        }

        user.PhoneNumberConfirmed = true;
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
