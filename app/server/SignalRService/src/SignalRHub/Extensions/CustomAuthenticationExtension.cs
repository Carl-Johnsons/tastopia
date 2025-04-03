using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SignalRHub.Extensions;

public static class CustomAuthenticationExtension
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, string hubEndPoint)
    {
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                // Clear default Microsoft's JWT claim mapping
                // Ref: https://stackoverflow.com/questions/70766577/asp-net-core-jwt-token-is-transformed-after-authentication
                options.MapInboundClaims = false;
                options.SaveToken = true;

                // Completely disable token validations
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = false,
                    RequireSignedTokens = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero,
                    /*  
                     *  Return JwtSecurityToken(token) will cause this error
                     *  
                     *  Microsoft.IdentityModel.Tokens.SecurityTokenInvalidSignatureException: IDX10506: Signature validation failed.
                     *  The user defined 'Delegate' specified on TokenValidationParameters did not return a 'Microsoft.IdentityModel.JsonWebTokens.JsonWebToken',
                     *  but returned a 'System.IdentityModel.Tokens.Jwt.JwtSecurityToken' when validating token: '[PII of type 'Microsoft.IdentityModel.JsonWebTokens.
                     *  JsonWebToken' is hidden. For more details, see https://aka.ms/IdentityModel/PII.]'
                    */
                    SignatureValidator = (token, parameters) => new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(token)
                };
                // For development only
                options.IncludeErrorDetails = true;


                // We have to hook the OnMessageReceived event in order to
                // allow the JWT authentication handler to read the access
                // token from the query string when a WebSocket or 
                // Server-Sent Events request comes in.

                // Sending the access token in the query string is required when using WebSockets or ServerSentEvents
                // due to a limitation in Browser APIs. We restrict it to only calls to the
                // SignalR hub in this code.
                // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
                // for more information about security considerations when using
                // the query string to transmit the access token.
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments(hubEndPoint))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };

            });
        return services;
    }
}
