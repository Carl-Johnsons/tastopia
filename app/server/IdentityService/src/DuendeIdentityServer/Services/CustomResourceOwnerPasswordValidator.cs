using Contract.Constants;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;
using static IdentityModel.OidcConstants;

namespace DuendeIdentityServer.Services;

public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly SignInManager<ApplicationAccount> _signInManager;

    public CustomResourceOwnerPasswordValidator(UserManager<ApplicationAccount> userManager, SignInManager<ApplicationAccount> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    // User name can be username or email or phone
    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        ApplicationAccount? user;
        if (context.UserName.Contains("@"))
        {
            user = await _userManager.FindByEmailAsync(context.UserName);
        }
        else if (context.UserName.All(char.IsDigit))
        {
            user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == context.UserName);
        }
        else
        {
            user = await _userManager.FindByNameAsync(context.UserName);
        }

        if (user == null)
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid credentials");
            return;
        }
        var roles = await _userManager.GetRolesAsync(user);
        if (roles[0] != Roles.Code.USER.ToString())
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient, "Permission Denied");
            return;
        }

        var result = await _signInManager.PasswordSignInAsync(user, context.Password, isPersistent: true, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient, "Invalid Credentials");
            return;
        }

        if (user.IsFirstTimeLogin)
        {
            user.IsFirstTimeLogin = false;
            await _userManager.UpdateAsync(user);
        }
        var claims = await _userManager.GetClaimsAsync(user);
        context.Result = new GrantValidationResult(
                subject: user.Id.ToString(),
                authenticationMethod: AuthenticationMethods.Password,
                claims: claims
            );
    }
}
