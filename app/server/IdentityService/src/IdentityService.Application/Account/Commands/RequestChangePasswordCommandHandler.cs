using Contract.Constants;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Account.Commands;

public record RequestChangePasswordCommand : IRequest<Result>
{
    public string Identifier { get; set; } = null!;
    public AccountMethod Method { get; set; }
}

public class RequestChangePasswordCommandHandler : IRequestHandler<RequestChangePasswordCommand, Result>
{
    public UserManager<ApplicationAccount> _userManager { get; set; }
    private IServiceBus _serviceBus { get; set; }


    public RequestChangePasswordCommandHandler(UserManager<ApplicationAccount> userManager,
                                        IServiceBus serviceBus)
    {
        _userManager = userManager;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(RequestChangePasswordCommand request, CancellationToken cancellationToken)
    {
        ApplicationAccount? account;

        switch (request.Method)
        {
            case AccountMethod.Email:
                account = await _userManager.Users.SingleOrDefaultAsync(a => (a.Email ?? "").ToLower() == request.Identifier.ToLower());
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
        var OTP = OTPUtility.GenerateNumericOTP();

        account.ForgotPasswordOTP = OTP;
        account.ForgotPasswordCreated = DateTime.UtcNow;
        account.ForgotPasswordExpiry = DateTime.UtcNow.AddMinutes(5);
        await _userManager.UpdateAsync(account);

        await _serviceBus.Publish(new UserSendOTPEvent
        {
            AccountId = Guid.Parse(account.Id),
            Identifier = request.Method == AccountMethod.Email ? account.Email! : account.PhoneNumber!,
            Method = request.Method,
            OTP = OTP,
            OTPMethod = OTPMethod.ForgotPassword
        });

        return Result.Success();
    }
}
