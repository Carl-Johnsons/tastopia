using Contract.Constants;
using IdentityService.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Application.Account.Queries;

public record CheckUpdateIdentifierOTPQuery : IRequest<Result>
{
    public Guid AccountId { get; set; }
    public string Identifier { get; set; } = null!;
    public string OTP { get; set; } = null!;
    public AccountMethod Method { get; set; }
}


public class CheckUpdateIdentifierOTPQueryHandler : IRequestHandler<CheckUpdateIdentifierOTPQuery, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly IApplicationDbContext _context;

    public CheckUpdateIdentifierOTPQueryHandler(UserManager<ApplicationAccount> userManager, IApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<Result> Handle(CheckUpdateIdentifierOTPQuery request, CancellationToken cancellationToken)
    {
        if (request.Method == AccountMethod.Email) return await CheckUpdateEmail(request, cancellationToken);
        if (request.Method == AccountMethod.Phone) return await CheckUpdatePhone(request, cancellationToken);
        return Result.Failure(AccountError.VerifyFailed, "Wrong account method");
    }

    private async Task<Result> CheckUpdateEmail(CheckUpdateIdentifierOTPQuery request, CancellationToken cancellationToken)
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

        return Result.Success();
    }
    private async Task<Result> CheckUpdatePhone(CheckUpdateIdentifierOTPQuery request, CancellationToken cancellationToken)
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

        return Result.Success();
    }
}

