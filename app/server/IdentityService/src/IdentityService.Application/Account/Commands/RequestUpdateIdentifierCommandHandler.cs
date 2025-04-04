using Contract.Constants;
using IdentityService.Domain.Interfaces;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Account.Commands;

public record RequestUpdateIdentifierCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Identifier { get; set; } = null!;
    public AccountMethod Method { get; set; }
}

public class RequestUpdateIdentifierCommandHandler : IRequestHandler<RequestUpdateIdentifierCommand, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IServiceBus _serviceBus;

    public RequestUpdateIdentifierCommandHandler(UserManager<ApplicationAccount> userManager,
                                       IApplicationDbContext context,
                                       IServiceBus serviceBus)
    {
        _userManager = userManager;
        _context = context;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(RequestUpdateIdentifierCommand request, CancellationToken cancellationToken)
    {
        switch (request.Method)
        {
            case AccountMethod.Email:
                return await RequestUpdateEmail(request, cancellationToken);
            case AccountMethod.Phone:
                return await RequestUpdatePhone(request, cancellationToken);
            default:
                return Result.Failure(AccountError.UnlinkAccountFailed, "Wrong account method");
        }
    }

    public async Task<Result> RequestUpdateEmail(RequestUpdateIdentifierCommand request, CancellationToken cancellationToken)
    {
        var account = await _userManager.Users.SingleOrDefaultAsync(a => a.Id == request.Id.ToString());
        if (account == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        var OTP = OTPUtility.GenerateNumericOTP();

        account.EmailOTP = OTP;
        account.EmailOTPCreated = DateTime.UtcNow;
        account.EmailOTPExpiry = DateTime.UtcNow.AddMinutes(5);
        await _userManager.UpdateAsync(account);

        await _serviceBus.Publish(new UserSendOTPEvent
        {
            AccountId = Guid.Parse(account.Id),
            Identifier = request.Identifier,
            Method = request.Method,
            OTP = OTP,
            OTPMethod = OTPMethod.UpdateIdentifier,
        });
        return Result.Success();
    }

    public async Task<Result> RequestUpdatePhone(RequestUpdateIdentifierCommand request, CancellationToken cancellationToken)
    {
        var account = await _userManager.Users.SingleOrDefaultAsync(a => a.Id == request.Id.ToString());
        if (account == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        var OTP = OTPUtility.GenerateNumericOTP();

        account.PhoneOTP = OTP;
        account.PhoneOTPCreated = DateTime.UtcNow;
        account.PhoneOTPExpiry = DateTime.UtcNow.AddMinutes(5);
        await _userManager.UpdateAsync(account);
        await _serviceBus.Publish(new UserSendOTPEvent
        {
            AccountId = Guid.Parse(account.Id),
            Identifier = request.Identifier,
            Method = request.Method,
            OTP = OTP,
            OTPMethod = OTPMethod.UpdateIdentifier,
        });
        return Result.Success();
    }
}
