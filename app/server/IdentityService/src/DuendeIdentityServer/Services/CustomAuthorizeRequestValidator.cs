using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Validation;
using IdentityModel;

namespace DuendeIdentityServer.Services;

public class CustomAuthorizeRequestValidator : ICustomAuthorizeRequestValidator
{
    public Task ValidateAsync(CustomAuthorizeRequestValidationContext context)
    {
        var request = context.Result.ValidatedRequest;
        if (string.IsNullOrWhiteSpace(request.Raw["prompted"]))
        {
            request.Raw.Add("prompted", "true");
            request.PromptModes = [OidcConstants.PromptModes.Login];
        }
        else if (request.Subject.IsAuthenticated())
        {
            request.PromptModes = [OidcConstants.PromptModes.None];
        }
        return Task.CompletedTask;
    }
}