using Contract.Interfaces;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using IdentityService.Application.Account.Commands;
using IdentityService.Application.Account.Queries;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using UserProto;

namespace DuendeIdentityServer.Pages.Account.VerifyForgotPassword;

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
    private readonly IServiceBus _serviceBus;
    private readonly ISender _sender;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public ViewModel View { get; set; } = default!;

    [BindProperty]
    public InputModel Input { get; set; } = default!;

    public Index(
        IIdentityServerInteractionService interaction,
        IAuthenticationSchemeProvider schemeProvider,
        IIdentityProviderStore identityProviderStore,
        IEventService events,
        UserManager<ApplicationAccount> userManager,
        SignInManager<ApplicationAccount> signInManager,
        IServiceBus serviceBus,
        ISender sender,
        GrpcUser.GrpcUserClient grpcUserClient)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _interaction = interaction;
        _schemeProvider = schemeProvider;
        _identityProviderStore = identityProviderStore;
        _events = events;
        _serviceBus = serviceBus;
        _sender = sender;
        _grpcUserClient = grpcUserClient;
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
        View = new ViewModel
        {
            IsValidOTP = false,
        };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        Console.WriteLine(JsonConvert.SerializeObject(Input, Formatting.Indented));
        Console.WriteLine(JsonConvert.SerializeObject(View, Formatting.Indented));
        // check if we are in the context of an authorization request
        var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        // the user clicked the "cancel" button
        if (Input.Button != "Verify" && Input.Button != "ChangePassword" && Input.Button != "Resend")
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
            switch (Input.Button)
            {
                case "Verify":
                    try
                    {
                        var result = await _sender.Send(new CheckForgotPasswordOTPQuery
                        {
                            Identifier = Input.Identifier,
                            OTP = Input.OTP,
                            Method = IdentifierUtility.Check(Input.Identifier)
                        });

                        result.ThrowIfFailure();

                        View = new ViewModel
                        {
                            IsValidOTP = true
                        };
                        return Page();
                    }
                    catch (ResultException rex)
                    {
                        if (rex != null)
                        {
                            ModelState.AddModelError(string.Empty, rex.Errors.ElementAt(0)?.Message ?? "Error! Please try again");
                            View = new ViewModel
                            {
                                IsValidOTP = true
                            };
                            return Page();
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;
                case "ReturnVerify":
                    View = new ViewModel
                    {
                        IsValidOTP = false
                    };
                    return Page();
                case "Resend":
                    try
                    {
                        var result = await _sender.Send(new RequestChangePasswordCommand
                        {
                            Identifier = Input.Identifier,
                            Method = IdentifierUtility.Check(Input.Identifier)
                        });

                        result.ThrowIfFailure();

                        View = new ViewModel
                        {
                            IsValidOTP = false
                        };
                        return Page();
                    }
                    catch (ResultException rex)
                    {
                        if (rex != null)
                        {
                            ModelState.AddModelError(string.Empty, rex.Errors.ElementAt(0)?.Message ?? "Error! Please try again");
                            View = new ViewModel
                            {
                                IsValidOTP = false
                            };
                            return Page();
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;
                case "ChangePassword":
                    try
                    {
                        if (Regex.IsMatch(Input.Password, "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^A-Za-z0-9]).{6,}$"))
                        {
                            ModelState.AddModelError("Input.Password", "Password must have length at least 6 and contain at least 1 uppercase, 1 lowercase, 1 number, 1 symbol");
                        }

                        if (Input.Password != Input.RetypePassword)
                        {
                            ModelState.AddModelError("Input.RetypePassword", "Password does not match!");
                        }

                        if (!ModelState.IsValid)
                        {
                            View = new ViewModel
                            {
                                IsValidOTP = true
                            };
                            return Page();
                        }
                        var result = await _sender.Send(new ChangePasswordCommand
                        {
                            Identifier = Input.Identifier,
                            OTP = Input.OTP,
                            Method = IdentifierUtility.Check(Input.Identifier),
                            Password = Input.Password
                        });

                        result.ThrowIfFailure();

                        View = new ViewModel
                        {
                            IsValidOTP = true
                        };
                        string url = $"~/Account/ChangePasswordSuccess?returnUrl={Uri.EscapeDataString(Input.ReturnUrl!)}";

                        return Redirect(url);
                    }
                    catch (ResultException rex)
                    {
                        if (rex != null)
                        {
                            ModelState.AddModelError(string.Empty, rex.Errors.ElementAt(0)?.Message ?? "Error! Please try again");
                            View = new ViewModel
                            {
                                IsValidOTP = true
                            };
                            return Page();
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;
                default:
                    break;
            }
            if (Input.Button == "Verify")
            {

            }
            else if (Input.Button == "ChangePassword")
            {

            }
            ModelState.AddModelError(string.Empty, Options.NotFound);
        }

        // something went wrong, show form with error
        View = new ViewModel
        {
            IsValidOTP = false
        };
        return Page();
    }
}
