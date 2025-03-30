using Contract.Constants;
using IdentityService.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
namespace IdentityService.Application.Account.Commands;

public record VerifyAccountCommand : IRequest<Result>
{
    public Guid AccountId { get; set; }
    public string OTP { get; set; } = null!;
    public AccountMethod Method { get; set; }
}


public class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly IApplicationDbContext _context;

    public VerifyAccountCommandHandler(UserManager<ApplicationAccount> userManager, IApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<Result> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
    {
        if (request.Method == AccountMethod.Email) return await VerifyEmail(request, cancellationToken);
        if (request.Method == AccountMethod.Phone) return await VerifyPhone(request, cancellationToken);
        return Result.Failure(AccountError.VerifyFailed, "Wrong account method");
    }

    private async Task<Result> VerifyEmail(VerifyAccountCommand request, CancellationToken cancellationToken)
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

        // The expiry store in database in UTC time but when it return value it return local time
        if (user.EmailOTPExpiry < DateTime.Now)
        {
            return Result.Failure(AccountError.OTPExpired);
        }

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
    private async Task<Result> VerifyPhone(VerifyAccountCommand request, CancellationToken cancellationToken)
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
