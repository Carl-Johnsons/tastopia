using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
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
            Log.Information("Login to google instead default :)");

            var externalProvider = "Google";
            var codeChallenge = request.Raw.Get("code_challenge");
            var codeChallengeMethod = request.Raw.Get("code_challenge_method");

            Log.Information("Authorize request details: ClientId={ClientId}, RedirectUri={RedirectUri}, ResponseType={ResponseType}, State={State}, RequestedScopes={RequestedScopes}",
                           request.ClientId, request.RedirectUri, request.ResponseType, request.State, string.Join(", ", request.RequestedScopes));


            var returnUrl = new StringBuilder("/connect/authorize/callback?");
            returnUrl.Append($"response_type={HttpUtility.UrlEncode(request.ResponseType)}");
            returnUrl.Append($"&client_id={HttpUtility.UrlEncode(request.ClientId)}");
            returnUrl.Append($"&state={HttpUtility.UrlEncode(request.State)}");
            returnUrl.Append($"&scope={HttpUtility.UrlEncode(string.Join(" ", request.RequestedScopes))}");
            returnUrl.Append($"&redirect_uri={HttpUtility.UrlEncode(request.RedirectUri)}");

            if (!string.IsNullOrEmpty(codeChallenge))
            {
                returnUrl.Append($"&code_challenge={HttpUtility.UrlEncode(codeChallenge)}");
                returnUrl.Append($"&code_challenge_method={HttpUtility.UrlEncode(codeChallengeMethod)}");
            }

            Log.Information("Constructed Return URL: {ReturnUrl}", returnUrl.ToString());

            //var returnUrl = $"%2Fconnect%2Fauthorize%2Fcallback%3Fresponse_type%3Dcode%26client_id%3Dreact.native%26state%3Df%26scope%3DIdentityServerApi%2520openid%2520profile%26redirect_uri%3Dhttps%253A%252F%252Fwww.getpostman.com%252Foauth2%252Fcallback%26code_challenge%3D{codeChallenge}%26code_challenge_method%3D{codeChallengeMethod}";
            var redirectUrl = HttpUtility.UrlPathEncode($"/ExternalLogin/Challenge?scheme={externalProvider}&returnUrl={HttpUtility.UrlEncode(returnUrl.ToString())}");
            Log.Information("Constructed redirect Url: " + redirectUrl);

            return new InteractionResponse
            {
                IsLogin = false, // Disable default login handling
                RedirectUrl = redirectUrl
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
