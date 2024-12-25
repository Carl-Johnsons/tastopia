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

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var result = await _signInManager.PasswordSignInAsync(context.UserName, context.Password, isPersistent: true, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(context.UserName);
            if (user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                context.Result = new GrantValidationResult(
                        subject: user.Id.ToString(),
                        authenticationMethod: AuthenticationMethods.Password,
                        claims: claims
                    );
                return;
            }
        }

        context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient, "Invalid Credentials");
    }
}
