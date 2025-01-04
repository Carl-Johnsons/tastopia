using Contract.Constants;
using IdentityService.Domain.Common;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Duende.IdentityServer.Models.IdentityResources;

namespace IdentityService.Application.Account.Commands;

public record LinkAccountCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Identifier { get; set; } = null!;
    public AccountMethod Method { get; set; }
}

public class LinkAccountCommandHandler : IRequestHandler<LinkAccountCommand, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly IServiceBus _serviceBus;

    public LinkAccountCommandHandler(UserManager<ApplicationAccount> userManager, IServiceBus serviceBus)
    {
        _userManager = userManager;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(LinkAccountCommand request, CancellationToken cancellationToken)
    {
        switch (request.Method)
        {
            case AccountMethod.Email:
                return await LinkEmail(request, cancellationToken);
            case AccountMethod.Phone:
                return await LinkPhone(request, cancellationToken);
            default:
                return Result.Failure(AccountError.CreateAccountFailed);
        }
    }

    public async Task<Result> LinkEmail(LinkAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _userManager.Users.SingleOrDefaultAsync(a => a.Id == request.Id.ToString());
        if (account == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (account.EmailConfirmed)
        {
            return Result.Failure(AccountError.EmailAlreadyConfirmed);
        }

        var isExistEmail = _userManager.Users.Any(a => a.Email == request.Identifier);

        if (account.Email != null || isExistEmail)
        {
            return Result.Failure(AccountError.EmailAlreadyExisted);
        }

        var OTP = OTPUtility.GenerateNumericOTP();

        account.Email = request.Identifier;
        account.EmailOTP = OTP;
        account.EmailOTPCreated = DateTime.UtcNow;
        account.EmailOTPExpiry = DateTime.UtcNow.AddMinutes(5);

        await _serviceBus.Publish(new LinkAccountEvent
        {
            AccountId = request.Id,
            Identifier = request.Identifier,
            Method = AccountMethod.Email,
            OTP = OTP
        });

        return Result.Success();
    }

    public async Task<Result> LinkPhone(LinkAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _userManager.Users.SingleOrDefaultAsync(a => a.Id == request.Id.ToString());
        if (account == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (account.PhoneNumberConfirmed)
        {
            return Result.Failure(AccountError.EmailAlreadyConfirmed);
        }

        var isExistPhone = _userManager.Users.Any(a => a.PhoneNumber == request.Identifier);

        if (account.PhoneNumber != null || isExistPhone)
        {
            return Result.Failure(AccountError.PhoneAlreadyExisted);
        }

        var OTP = OTPUtility.GenerateNumericOTP();

        account.PhoneNumber = request.Identifier;
        account.PhoneOTP = OTP;
        account.PhoneOTPCreated = DateTime.UtcNow;
        account.PhoneOTPExpiry = DateTime.UtcNow.AddMinutes(5);

        await _serviceBus.Publish(new LinkAccountEvent
        {
            AccountId = request.Id,
            Identifier = request.Identifier,
            Method = AccountMethod.Phone,
            OTP = OTP
        });

        return Result.Success();
    }
}
