using IdentityService.Domain.Common;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityService.Application.Account;

public record RegisterAccountCommand : IRequest<Result>
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Phone { get; set; } = null!;
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}

public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly SignInManager<ApplicationAccount> _signInManager;
    private readonly IServiceBus _serviceBus;

    public RegisterAccountCommandHandler(UserManager<ApplicationAccount> userManager, SignInManager<ApplicationAccount> signInManager, IServiceBus serviceBus)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        // Generate Email OTP
        var existingEmailSet = _userManager.Users
               .Select(account => account.EmailConfirmationOTP)
               .ToHashSet();
        string EmailOTP;

        do
        {
            EmailOTP = OTPUtility.GenerateAlphanumericOTP();
        } while (existingEmailSet.Contains(EmailOTP));

        var acc = new ApplicationAccount
        {
            Email = request.Email,
            PhoneNumber = request.Phone,
            EmailConfirmationOTP = EmailOTP,
            EmailConfirmationExpiry = DateTime.UtcNow.AddMinutes(5),
        };

        var result = await _userManager.CreateAsync(acc, request.Password);

        if (!result.Succeeded)
        {
            return Result.Failure(AccountError.CreateAccountFailed);
        }
        await _signInManager.SignInAsync(acc, isPersistent: false);

        await _serviceBus.Publish(new UserRegisterEvent
        {
            AccountId = Guid.Parse(acc.Id),
            Email = request.Email,
            Phone = request.Phone,
            EmailOTP = EmailOTP,
        });

        return Result.Success();
    }
}
