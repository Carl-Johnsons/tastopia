using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using Serilog;
using System.Security.Cryptography;
using System.Text;

namespace DuendeIdentityServer.Services;

public class CustomAuthorizeInteractionResponseGenerator : AuthorizeInteractionResponseGenerator
{
    public CustomAuthorizeInteractionResponseGenerator(IdentityServerOptions options, IClock clock, ILogger<AuthorizeInteractionResponseGenerator> logger, IConsentService consent, IProfileService profile) : base(options, clock, logger, consent, profile)
    {

    }
    protected override async Task<InteractionResponse> ProcessLoginAsync(ValidatedAuthorizeRequest request)
    {
        var response = await base.ProcessLoginAsync(request);

        if (response.IsLogin)
        {
            Log.Information("Login to google instead default :)");
            var redirectUri = request.RedirectUri;
            var responseType = request.ResponseType;

            var externalProvider = "Google";
            var codeChallenge = request.Raw.Get("code_challenge");
            var codeChallengeMethod = request.Raw.Get("code_challenge_method");

            if (!string.IsNullOrEmpty(codeChallenge))
            {
                Log.Information("Code Challenge: {CodeChallenge}", codeChallenge);
                Log.Information("Code Challenge Method: {CodeChallengeMethod}", codeChallengeMethod);
            }
            var endcodedReturnUrl = $"/connect/authorize/callback?response_type={responseType}&client_id=react.native&state=f&scope=IdentityServerApi%20openid%20profile&redirect_uri={redirectUri}&code_challenge={codeChallenge}&code_challenge_method";
            var returnUrl = $"%2Fconnect%2Fauthorize%2Fcallback%3Fresponse_type%3Dcode%26client_id%3Dreact.native%26state%3Df%26scope%3DIdentityServerApi%2520openid%2520profile%26redirect_uri%3Dhttps%253A%252F%252Fwww.getpostman.com%252Foauth2%252Fcallback%26code_challenge%3D{codeChallenge}%26code_challenge_method%3D{codeChallengeMethod}";
            Log.Information(endcodedReturnUrl);
            return new InteractionResponse
            {
                IsLogin = false, // Disable default login handling
                RedirectUrl = $"/ExternalLogin/Challenge?scheme={externalProvider}&returnUrl={endcodedReturnUrl}"
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
