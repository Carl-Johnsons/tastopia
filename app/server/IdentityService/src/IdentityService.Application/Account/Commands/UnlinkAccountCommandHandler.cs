﻿using Contract.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Account.Commands;

public record UnlinkAccountCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public AccountMethod Method { get; set; }
}

public class UnlinkAccountCommandHandler : IRequestHandler<UnlinkAccountCommand, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;

    public UnlinkAccountCommandHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(UnlinkAccountCommand request, CancellationToken cancellationToken)
    {
        switch (request.Method)
        {
            case AccountMethod.Email:
                return await UnlinkEmail(request, cancellationToken);
            case AccountMethod.Phone:
                return await UnlinkPhone(request, cancellationToken);
            default:
                return Result.Failure(AccountError.InvalidAccountMethod, "Wrong account method");
        }
    }

    public async Task<Result> UnlinkEmail(UnlinkAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _userManager.Users.SingleOrDefaultAsync(a => a.Id == request.Id.ToString());
        if (account == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (account.Email == null || account.PhoneNumber == null)
        {
            return Result.Failure(AccountError.OnlyExistOneIdentifier);
        }

        account.EmailConfirmed = false;
        account.Email = null;
        account.EmailOTPCreated = null;
        account.EmailOTPExpiry = null;
        account.RequestOTPCount = 0;
        var updateResult = await _userManager.UpdateAsync(account);

        return Result.Success();
    }

    public async Task<Result> UnlinkPhone(UnlinkAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _userManager.Users.SingleOrDefaultAsync(a => a.Id == request.Id.ToString());
        if (account == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (account.Email == null || account.PhoneNumber == null)
        {
            return Result.Failure(AccountError.OnlyExistOneIdentifier);
        }

        account.PhoneNumber = null;
        account.PhoneNumberConfirmed = false;
        account.PhoneOTPCreated = null;
        account.PhoneOTPExpiry = null;
        var updateResult = await _userManager.UpdateAsync(account);

        return Result.Success();
    }
}
