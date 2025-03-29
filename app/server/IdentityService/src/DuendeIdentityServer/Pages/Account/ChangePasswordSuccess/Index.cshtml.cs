using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace DuendeIdentityServer.Pages.Account.ChangePasswordSuccess;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    public Index()
    {
    }
    public async Task<IActionResult> OnGet(string returnUrl)
    {
        var encodedRedirectUri = WebUtility.UrlEncode(returnUrl);

        ViewData["ReturnUrl"] = encodedRedirectUri;
        return Page();
    }
}
