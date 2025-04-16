using Contract.Constants;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Account.Commands;

public class ResendOTPCommand : IRequest<Result>
{
    public Guid AccountId { get; set; }
    public AccountMethod Method { get; set; }
}

public class ResendOTPCommandHandler : IRequestHandler<ResendOTPCommand, Result>
{
    private UserManager<ApplicationAccount> _userManager { get; set; }
    private IServiceBus _serviceBus { get; set; }

    public ResendOTPCommandHandler(UserManager<ApplicationAccount> userManager, IServiceBus serviceBus)
    {
        _userManager = userManager;
        _serviceBus = serviceBus;
    }


    public async Task<Result> Handle(ResendOTPCommand request, CancellationToken cancellationToken)
    {
        switch (request.Method)
        {
            case AccountMethod.Email:
                return await ResendEmailOTP(request, cancellationToken);
            case AccountMethod.Phone:
                return await ResendPhoneOTP(request, cancellationToken);
            default:
                return Result.Failure(AccountError.InvalidAccountMethod, "Wrong account method");
        }
    }

    private async Task<Result> ResendEmailOTP(ResendOTPCommand request, CancellationToken cancellationToken)
    {
        var acc = await _userManager.Users.SingleOrDefaultAsync(a => a.Id == request.AccountId.ToString());
        if (acc == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (acc.Email == null)
        {
            return Result.Failure(AccountError.EmailNotFound);
        }

        var OTP = OTPUtility.GenerateNumericOTP();
        acc.RequestOTPCount += 1;
        acc.EmailOTP = OTP;
        acc.EmailOTPCreated = DateTime.UtcNow;
        acc.EmailOTPExpiry = DateTime.UtcNow.AddMinutes(5);

        var result = await _userManager.UpdateAsync(acc);

        if (!result.Succeeded)
        {
            return Result.Failure(AccountError.ResendOTPFailed);
        }

        await _serviceBus.Publish(new UserSendOTPEvent
        {
            AccountId = request.AccountId,
            Identifier = acc.Email ?? "",
            Method = AccountMethod.Email,
            OTP = OTP,
            OTPMethod = OTPMethod.Resend
        });
        return Result.Success();
    }

    private async Task<Result> ResendPhoneOTP(ResendOTPCommand request, CancellationToken cancellationToken)
    {
        var acc = await _userManager.Users.SingleOrDefaultAsync(a => a.Id == request.AccountId.ToString());
        if (acc == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (acc.PhoneNumber == null)
        {
            return Result.Failure(AccountError.PhoneNotFound);
        }

        var OTP = OTPUtility.GenerateNumericOTP();
        acc.RequestOTPCount += 1;
        acc.PhoneOTP = OTP;
        acc.PhoneOTPCreated = DateTime.UtcNow;
        acc.PhoneOTPExpiry = DateTime.UtcNow.AddMinutes(5);

        var result = await _userManager.UpdateAsync(acc);

        if (!result.Succeeded)
        {
            return Result.Failure(AccountError.ResendOTPFailed);
        }

        await _serviceBus.Publish(new UserSendOTPEvent
        {
            AccountId = request.AccountId,
            Identifier = acc.PhoneNumber ?? "",
            Method = AccountMethod.Phone,
            OTP = OTP,
            OTPMethod = OTPMethod.Resend
        });
        return Result.Success();
    }
}
