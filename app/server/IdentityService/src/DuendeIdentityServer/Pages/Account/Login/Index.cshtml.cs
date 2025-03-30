// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Contract.Constants;
using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace DuendeIdentityServer.Pages.Account.Login;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly SignInManager<ApplicationAccount> _signInManager;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IEventService _events;
    private readonly IAuthenticationSchemeProvider _schemeProvider;
    private readonly IIdentityProviderStore _identityProviderStore;

    public ViewModel View { get; set; } = default!;

    [BindProperty]
    public InputModel Input { get; set; } = default!;

    public Index(
        IIdentityServerInteractionService interaction,
        IAuthenticationSchemeProvider schemeProvider,
        IIdentityProviderStore identityProviderStore,
        IEventService events,
        UserManager<ApplicationAccount> userManager,
        SignInManager<ApplicationAccount> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _interaction = interaction;
        _schemeProvider = schemeProvider;
        _identityProviderStore = identityProviderStore;
        _events = events;
    }

    public async Task<IActionResult> OnGet(string? returnUrl)
    {
        await BuildModelAsync(returnUrl);

        var encodedRedirectUri = WebUtility.UrlEncode(returnUrl);

        ViewData["ReturnUrl"] = encodedRedirectUri;

        // Check if there is an error message in TempData
        if (TempData["ErrorMessage"] != null)
        {
            ModelState.AddModelError(string.Empty, TempData["ErrorMessage"]?.ToString()!);
        }

        if (View.IsExternalLoginOnly)
        {
            // we only have one option for logging in and it's an external provider
            return RedirectToPage("/ExternalLogin/Challenge", new { scheme = View.ExternalLoginScheme, returnUrl });
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // check if we are in the context of an authorization request
        var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        // the user clicked the "cancel" button
        if (Input.Button != "login")
        {
            if (context != null)
            {
                // This "can't happen", because if the ReturnUrl was null, then the context would be null
                ArgumentNullException.ThrowIfNull(Input.ReturnUrl, nameof(Input.ReturnUrl));

                // if the user cancels, send a result back into IdentityServer as if they 
                // denied the consent (even if this client does not require consent).
                // this will send back an access denied OIDC error response to the client.
                await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                if (context.IsNativeClient())
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage(Input.ReturnUrl);
                }

                return Redirect(Input.ReturnUrl ?? "~/");
            }
            else
            {
                // since we don't have a valid context, then we just go back to the home page
                return Redirect("~/");
            }
        }

        if (ModelState.IsValid)
        {
            ApplicationAccount? user = null;
            var method = IdentifierUtility.Check(Input.Username!);
            Console.WriteLine("METHOD");
            Console.WriteLine(method.ToString());
            Console.WriteLine(JsonConvert.SerializeObject(Input, Formatting.Indented));
            switch (method)
            {
                case AccountMethod.Phone:
                    user = await _userManager.Users.SingleOrDefaultAsync(a => a.PhoneNumber == Input.Username);
                    break;
                case AccountMethod.Email:
                    user = await _userManager.FindByEmailAsync(Input.Username!);
                    break;
                case AccountMethod.Username:
                    user = await _userManager.FindByNameAsync(Input.Username!);
                    break;
                default:
                    ModelState.AddModelError("InvalidMethod", Options.InvalidCredentialsMethod);
                    await BuildModelAsync(Input.ReturnUrl);
                    return Page();
            }

            if (user == null)
            {
                Console.WriteLine("Not found user");
                ModelState.AddModelError("InvalidCredential", Options.InvalidCredentialsErrorMessage);
                await BuildModelAsync(Input.ReturnUrl);
                return Page();
            }

            if (!user.IsActive)
            {
                Console.WriteLine("user not active");
                ModelState.AddModelError("Disabled", Options.AccountDisabledErrorMessage);
                // account disabled, show form with error
                await BuildModelAsync(Input.ReturnUrl);
                return Page();
            }

            bool isAllowed = false;

            foreach (var role in Roles.AllowedRoles.ADMIN)
            {
                isAllowed = isAllowed || await _userManager.IsInRoleAsync(user, role);
            }

            if (!isAllowed)
            {
                Console.WriteLine("Not allowed");
                ModelState.AddModelError("InvalidCredential", Options.InvalidCredentialsErrorMessage);
                await BuildModelAsync(Input.ReturnUrl);
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName!, Input.Password!, Input.RememberLogin, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                await _events.RaiseAsync(new UserLoginSuccessEvent(user!.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));
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
                await _events.RaiseAsync(new UserLoginFailureEvent(Input.Username, error, clientId: context?.Client.ClientId));
                Telemetry.Metrics.UserLoginFailure(context?.Client.ClientId, IdentityServerConstants.LocalIdentityProvider, error);
                ModelState.AddModelError(string.Empty, Options.InvalidCredentialsErrorMessage);
            }
        }

        // something went wrong, show form with error
        await BuildModelAsync(Input.ReturnUrl);
        return Page();
    }

    private async Task BuildModelAsync(string? returnUrl)
    {
        Input = new InputModel
        {
            ReturnUrl = returnUrl
        };

        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
        if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
        {
            var local = context.IdP == IdentityServerConstants.LocalIdentityProvider;

            // this is meant to short circuit the UI and only trigger the one external IdP
            View = new ViewModel
            {
                EnableLocalLogin = local,
            };

            Input.Username = context.LoginHint;

            if (!local)
            {
                View.ExternalProviders = new[] { new ViewModel.ExternalProvider(authenticationScheme: context.IdP) };
            }

            return;
        }

        var schemes = await _schemeProvider.GetAllSchemesAsync();

        var providers = schemes
            .Where(x => x.DisplayName != null)
            .Select(x => new ViewModel.ExternalProvider
            (
                authenticationScheme: x.Name,
                displayName: x.DisplayName ?? x.Name
            )).ToList();

        var dynamicSchemes = (await _identityProviderStore.GetAllSchemeNamesAsync())
            .Where(x => x.Enabled)
            .Select(x => new ViewModel.ExternalProvider
            (
                authenticationScheme: x.Scheme,
                displayName: x.DisplayName ?? x.Scheme
            ));
        providers.AddRange(dynamicSchemes);


        var allowLocal = true;
        var client = context?.Client;
        if (client != null)
        {
            allowLocal = client.EnableLocalLogin;
            if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Count != 0)
            {
                providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
            }
        }

        View = new ViewModel
        {
            AllowRememberLogin = Options.AllowRememberLogin,
            EnableLocalLogin = allowLocal && Options.AllowLocalLogin,
            ExternalProviders = providers.ToArray()
        };
    }
}
