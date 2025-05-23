using Contract.Constants;
using Duende.IdentityServer.Events;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityService.Application.Account.Commands;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DuendeIdentityServer.Pages.Account.ChangePassword;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly SignInManager<ApplicationAccount> _signInManager;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IEventService _events;
    private readonly ISender _sender;

    public ViewModel View { get; set; } = default!;

    [BindProperty]
    public InputModel Input { get; set; } = default!;

    public Index(
        IIdentityServerInteractionService interaction,
        IEventService events,
        UserManager<ApplicationAccount> userManager,
        SignInManager<ApplicationAccount> signInManager,
        ISender sender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _interaction = interaction;
        _events = events;
        _sender = sender;
    }

    public async Task<IActionResult> OnGet(string returnUrl, string identifier)
    {
        Input = new InputModel
        {
            ReturnUrl = returnUrl,
            Identifier = identifier
        };
        var encodedRedirectUri = WebUtility.UrlEncode(returnUrl);

        ViewData["ReturnUrl"] = encodedRedirectUri;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        // the user clicked the "cancel" button
        if (Input.Button == "Cancel")
        {
            return await DenyAuthorization(Input.ReturnUrl);
        }
        if (!ModelState.IsValid)
        {
            return Page();
        }
        if (Input.Button == "ChangePassword")
        {
            try
            {
                // if (Regex.IsMatch(Input.Password, "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^A-Za-z0-9]).{6,}$"))
                // {
                // ModelState.AddModelError("Input.Password", "Password must have length at least 6 and contain at least 1 uppercase, 1 lowercase, 1 number, 1 symbol");
                // }

                if (Input.Password != Input.RetypePassword)
                {
                    ModelState.AddModelError("Input.RetypePassword", "Password does not match!");
                }

                if (!ModelState.IsValid)
                {
                    return Page();
                }
                var result = await _sender.Send(new ChangePasswordFirstTimeLoginCommand
                {
                    Identifier = Input.Identifier!,
                    Password = Input.Password
                });

                result.ThrowIfFailure();

                ApplicationAccount? account = await _userManager.Users.SingleOrDefaultAsync(a => (a.UserName ?? "").ToLower() == (Input.Identifier ?? "").ToLower());

                if (account == null)
                {
                    throw new NullReferenceException("Account can't be null here");
                }

                var loginResult = await _signInManager.PasswordSignInAsync(account.UserName!, Input.Password!, isPersistent: true, lockoutOnFailure: false);
                if (loginResult.Succeeded)
                {
                    await _events.RaiseAsync(new UserLoginSuccessEvent(account!.UserName, account.Id, account.UserName, clientId: context?.Client.ClientId));

                    Telemetry.Metrics.UserLogin(context?.Client.ClientId, IdentityServerConstants.LocalIdentityProvider);

                    if (context != null)
                    {
                        // This "can't happen", because if the ReturnUrl was null, then the context would be null
                        ArgumentNullException.ThrowIfNull(Input.ReturnUrl, nameof(Input.ReturnUrl));

                        if (context.IsNativeClient())
                        {
                            // The client is native, so this change in how to
                            // return the response is for better UX for the end user.
                            return this.LoadingPage(Input.ReturnUrl);
                        }

                        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                        return Redirect(Input.ReturnUrl ?? "~/");
                    }

                    // request for a local page
                    if (Url.IsLocalUrl(Input.ReturnUrl))
                    {
                        return Redirect(Input.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(Input.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        // user might have clicked on a malicious link - should be logged
                        throw new ArgumentException("invalid return URL");
                    }
                }
                else
                {
                    const string error = "invalid credentials";
                    await _events.RaiseAsync(new UserLoginFailureEvent(account.UserName, error, clientId: context?.Client.ClientId));
                    Telemetry.Metrics.UserLoginFailure(context?.Client.ClientId, IdentityServerConstants.LocalIdentityProvider, error);
                    ModelState.AddModelError(string.Empty, Options.InvalidCredentialError);
                }
            }
            catch (ResultException rex)
            {
                if (rex != null)
                {
                    ModelState.AddModelError(string.Empty, rex.Errors.ElementAt(0)?.Message ?? "Error! Please try again");
                    return Page();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // something went wrong, show form with error
        return Page();
    }

    private async Task<IActionResult> DenyAuthorization(string? returnUrl)
    {
        // check if we are in the context of an authorization request
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
        if (context != null)
        {
            // This "can't happen", because if the ReturnUrl was null, then the context would be null
            ArgumentNullException.ThrowIfNull(returnUrl, nameof(returnUrl));

            // if the user cancels, send a result back into IdentityServer as if they 
            // denied the consent (even if this client does not require consent).
            // this will send back an access denied OIDC error response to the client.
            await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

            // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
            if (context.IsNativeClient())
            {
                // The client is native, so this change in how to
                // return the response is for better UX for the end user.
                return this.LoadingPage(returnUrl);
            }

            return Redirect(returnUrl ?? "~/");
        }
        else
        {
            // since we don't have a valid context, then we just go back to the home page
            return Redirect("~/");
        }
    }
}
