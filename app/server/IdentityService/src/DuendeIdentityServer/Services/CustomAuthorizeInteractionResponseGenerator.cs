using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using Serilog;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DuendeIdentityServer.Services;

public class CustomAuthorizeInteractionResponseGenerator : AuthorizeInteractionResponseGenerator
{
    public CustomAuthorizeInteractionResponseGenerator(IdentityServerOptions options, IClock clock, ILogger<AuthorizeInteractionResponseGenerator> logger, IConsentService consent, IProfileService profile) : base(options, clock, logger, consent, profile)
    {

    }
    protected override async Task<InteractionResponse> ProcessLoginAsync(ValidatedAuthorizeRequest request)
    {
        var response = await base.ProcessLoginAsync(request);

        if (response.IsLogin && request.ClientId == "react.native")
        {
            Log.Information("Login to provider instead default login page:)");

            var externalProvider = "Google";
            var codeChallenge = request.Raw.Get("code_challenge");
            var codeChallengeMethod = request.Raw.Get("code_challenge_method");

            Log.Information("Authorize request details: ClientId={ClientId}, RedirectUri={RedirectUri}, ResponseType={ResponseType}, State={State}, RequestedScopes={RequestedScopes}",
                           request.ClientId, request.RedirectUri, request.ResponseType, request.State, string.Join(", ", request.RequestedScopes));

            var callbackPath = "/connect/authorize/callback";
            var qsParams = new Dictionary<string, string?>
            {
                ["response_type"] = request.ResponseType,
                ["client_id"] = request.ClientId,
                ["state"] = request.State,
                ["scope"] = string.Join(" ", request.RequestedScopes),
                ["redirect_uri"] = request.RedirectUri,
                ["code_challenge"] = request.Raw.Get("code_challenge"),
                ["code_challenge_method"] = request.Raw.Get("code_challenge_method")
            };
            
            var returnUrl = QueryHelpers.AddQueryString(callbackPath, qsParams);

            Log.Information("Constructed Return URL: {ReturnUrl}", returnUrl.ToString());

            var externalParams = new Dictionary<string, string?>
            {
                ["scheme"] = externalProvider,
                ["returnUrl"] = returnUrl
            };
            var redirectUrl = QueryHelpers.AddQueryString("/ExternalLogin/Challenge", externalParams);
            Log.Information("Constructed redirect Url: " + redirectUrl);

            // Redirect to the external provider
            return new InteractionResponse
            {
                IsLogin = false,
                RedirectUrl = redirectUrl,
            };
        }

        return response;
    }

    private string GenerateCodeChallenge(string codeVerifier)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.ASCII.GetBytes(codeVerifier);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }
}
