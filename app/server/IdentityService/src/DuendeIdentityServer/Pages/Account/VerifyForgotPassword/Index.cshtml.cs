using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityService.Application.Account.Commands;
using IdentityService.Application.Account.Queries;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using UserProto;

namespace DuendeIdentityServer.Pages.Account.VerifyForgotPassword;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly ISender _sender;

    public ViewModel View { get; set; } = default!;

    [BindProperty]
    public InputModel Input { get; set; } = default!;

    public Index(
        IIdentityServerInteractionService interaction,
        ISender sender)
    {
        _interaction = interaction;
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
        View = new ViewModel
        {
            IsValidOTP = false,
        };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // the user clicked the "cancel" button
        if (Input.Button == "Cancel")
        {
            return await DenyAuthorization(Input.ReturnUrl);
        }
        if (ModelState.IsValid)
        {
            switch (Input.Button)
            {
                case "Verify":
                    try
                    {
                        if (string.IsNullOrEmpty(Input.OTP))
                        {
                            View = new ViewModel
                            {
                                IsValidOTP = false
                            };
                            ModelState.AddModelError("Input.OTP", Options.OTPRequired);
                            return Page();
                        }

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
                        // if (Regex.IsMatch(Input.Password, "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^A-Za-z0-9]).{6,}$"))
                        // {
                        // ModelState.AddModelError("Input.Password", "Password must have length at least 6 and contain at least 1 uppercase, 1 lowercase, 1 number, 1 symbol");
                        // }

                        if (Input.Password != Input.RetypePassword)
                        {
                            ModelState.AddModelError("Input.RetypePassword", "Password does not match!");
                        }

                        if (string.IsNullOrEmpty(Input.Password))
                        {
                            ModelState.AddModelError("Input.Password", "The Password field is required");
                        }

                        if (string.IsNullOrEmpty(Input.RetypePassword))
                        {
                            ModelState.AddModelError("Input.RetypePassword", "The Retype password field is required");
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
                            OTP = Input.OTP!,
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
        }

        // something went wrong, show form with error
        switch (Input.Button)
        {
            case "Verify":
            case "Resend":
                View = new ViewModel
                {
                    IsValidOTP = false
                };
                break;
            case "ReturnVerify":
                return RedirectToPage(new
                {
                    returnUrl = Input.ReturnUrl,
                    identifier = Input.Identifier
                });
            case "ChangePassword":
                View = new ViewModel
                {
                    IsValidOTP = true
                };
                break;
        }
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
