using Contract.Constants;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Google.Protobuf.Collections;
using IdentityService.Application.Account.Commands;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserProto;

namespace DuendeIdentityServer.Pages.Account.ForgotPassword;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly ISender _sender;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public ViewModel View { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public InputModel Input { get; set; } = default!;

    public Index(
        IIdentityServerInteractionService interaction,
        UserManager<ApplicationAccount> userManager,
        ISender sender,
        GrpcUser.GrpcUserClient grpcUserClient)
    {
        _userManager = userManager;
        _interaction = interaction;
        _sender = sender;
        _grpcUserClient = grpcUserClient;
    }

    public async Task<IActionResult> OnGet(string? identifier)
    {
        // the user clicked the "cancel" button
        if (Input.Button == "Cancel")
        {
            return await DenyAuthorization(Input.ReturnUrl);
        }
        if (string.IsNullOrEmpty(Input.Identifier) && Input.IsDirty)
        {
            ModelState.AddModelError("Input.Identifier", Options.IdentifierRequired);
        }

        if (!string.IsNullOrEmpty(Input.Identifier))
        {
            var normalizedIdentifier = Input.Identifier.ToLower();

            var acc = await _userManager.Users.SingleOrDefaultAsync(a => (a.Email ?? "").ToLower() == normalizedIdentifier || a.PhoneNumber == normalizedIdentifier);

            if (acc == null)
            {
                ModelState.AddModelError(string.Empty, Options.NotFound);
                return Page();
            }

            var role = await _userManager.GetRolesAsync(acc);
            if (role[0].ToLower() != Roles.Code.USER.ToString().ToLower())
            {
                ModelState.AddModelError(string.Empty, Options.NotFound);
                return Page();
            }

            var repeatedField = new RepeatedField<string>
            {
                acc.Id.ToString()
            };

            var grpcResponse = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
            {
                AccountId = { repeatedField }
            });

            View = new ViewModel
            {
                Identifier = Input.Identifier,
                User = new Contract.DTOs.UserDTO.SimpleUser
                {
                    AccountId = Guid.Parse(acc.Id),
                    AccountUsername = grpcResponse.Users[acc.Id].AccountUsername,
                    AvtUrl = grpcResponse.Users[acc.Id].AvtUrl,
                    DisplayName = grpcResponse.Users[acc.Id].DisplayName
                }
            };
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // the user clicked the "cancel" button
        if (Input.Button != "Recover" && Input.Button != "ReturnFind")
        {
            return await DenyAuthorization(Input.ReturnUrl);
        }

        if (ModelState.IsValid)
        {
            switch (Input.Button)
            {
                case "Recover":
                    try
                    {
                        if (string.IsNullOrEmpty(Input.Identifier))
                        {
                            ModelState.AddModelError("Input.Identifier", Options.IdentifierRequired);
                            return Page();
                        }
                        var method = IdentifierUtility.Check(Input.Identifier);

                        var result = await _sender.Send(new RequestChangePasswordCommand
                        {
                            Identifier = Input.Identifier,
                            Method = method
                        });
                        result.ThrowIfFailure();
                        string url = $"~/Account/VerifyForgotPassword?returnUrl={Uri.EscapeDataString(Input.ReturnUrl!)}&identifier={Uri.EscapeDataString(Input.Identifier)}";
                        return Redirect(url);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Send OTP failed! Please try again");
                    }
                    break;
                case "ReturnFind":
                    View = new ViewModel
                    {
                        Identifier = Input.Identifier,
                        User = null
                    };
                    break;
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
