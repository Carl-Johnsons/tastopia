using Contract.Constants;
using IdentityService.Domain.Interfaces;
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
    private readonly IApplicationDbContext _context;

    public UnlinkAccountCommandHandler(UserManager<ApplicationAccount> userManager,
                                       IApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
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

        if (!account.PhoneNumberConfirmed)
        {
            return Result.Failure(AccountError.PhoneNotConfirmed);
        }

        account.EmailConfirmed = false;
        account.Email = null;
        account.EmailOTPCreated = null;
        account.EmailOTPExpiry = null;
        account.RequestOTPCount = 0;
        var updateResult = await _userManager.UpdateAsync(account);

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

        if (!account.EmailConfirmed)
        {
            return Result.Failure(AccountError.EmailNotConfirmed);
        }

        account.PhoneNumber = null;
        account.PhoneNumberConfirmed = false;
        account.PhoneOTPCreated = null;
        account.PhoneOTPExpiry = null;
        var updateResult = await _userManager.UpdateAsync(account);

        return Result.Success();
    }
}
