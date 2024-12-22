using IdentityService.Domain.Common;
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

    public RegisterAccountCommandHandler(UserManager<ApplicationAccount> userManager, SignInManager<ApplicationAccount> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationAccount
        {
            Email = request.Email,
            PhoneNumber = request.Phone,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return Result.Failure(AccountError.CreateAccountFailed);
        }
        await _signInManager.SignInAsync(user, isPersistent: false);
        return Result.Success();
    }
}
