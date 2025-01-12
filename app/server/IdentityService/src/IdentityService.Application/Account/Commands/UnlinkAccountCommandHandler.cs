using Contract.Constants;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Account.Commands;

public record UnlinkAccountCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Identifier { get; set; } = null!;
    public AccountMethod Method { get; set; }
}

public class UnlinkAccountCommandHandler : IRequestHandler<LinkAccountCommand, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly IServiceBus _serviceBus;

    public UnlinkAccountCommandHandler(UserManager<ApplicationAccount> userManager, IServiceBus serviceBus)
    {
        _userManager = userManager;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(LinkAccountCommand request, CancellationToken cancellationToken)
    {
        switch (request.Method)
        {
            case AccountMethod.Email:
                return await UnlinkEmail(request, cancellationToken);
            case AccountMethod.Phone:
                return await UnlinkPhone(request, cancellationToken);
            default:
                return Result.Failure(AccountError.UnlinkAccountFailed, "Wrong account method");
        }
    }

    public async Task<Result> UnlinkEmail(LinkAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _userManager.Users.SingleOrDefaultAsync(a => a.Id == request.Id.ToString());
        if (account == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (!account.EmailConfirmed)
        {
            return Result.Failure(AccountError.EmailNotConfirmed);
        }

        var OTP = OTPUtility.GenerateNumericOTP();

        account.UnlinkEmailOTP = OTP;
        account.EmailOTPCreated = DateTime.UtcNow;
        account.EmailOTPExpiry = DateTime.UtcNow.AddMinutes(5);

        await _serviceBus.Publish(new UnlinkAccountEvent
        {
            AccountId = request.Id,
            Identifier = request.Identifier,
            Method = AccountMethod.Email,
            OTP = OTP
        });

        return Result.Success();
    }

    public async Task<Result> UnlinkPhone(LinkAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _userManager.Users.SingleOrDefaultAsync(a => a.Id == request.Id.ToString());
        if (account == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (!account.PhoneNumberConfirmed)
        {
            return Result.Failure(AccountError.PhoneNotConfirmed);
        }

        var OTP = OTPUtility.GenerateNumericOTP();

        account.UnlinkPhoneOTP = OTP;
        account.PhoneOTPCreated = DateTime.UtcNow;
        account.PhoneOTPExpiry = DateTime.UtcNow.AddMinutes(5);

        await _serviceBus.Publish(new UnlinkAccountEvent
        {
            AccountId = request.Id,
            Identifier = request.Identifier,
            Method = AccountMethod.Phone,
            OTP = OTP
        });

        return Result.Success();
    }
}
