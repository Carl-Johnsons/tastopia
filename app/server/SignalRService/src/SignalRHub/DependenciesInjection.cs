using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using SignalRHub.Filters;
using SignalRHub.Hubs;
using SignalRService.Infrastructure;

namespace SignalRHub;

public static class DependenciesInjection
{
    private static string HUB_ENDPOINT = "/tastopia-hub";
    public static WebApplicationBuilder AddChatHubServices(this WebApplicationBuilder builder)
    {
        SignalRService.Infrastructure.Utilities.EnvUtility.LoadEnvFile();

        var services = builder.Services;
        var host = builder.Host;

        services.AddInfrastructureServices(builder.Configuration);

        var reactUrl = DotNetEnv.Env.GetString("REACT_URL", "http://localhost:3000");

        host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

        services.AddHttpContextAccessor();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
         .AddJwtBearer(options =>
         {
             var IdentityDNS = DotNetEnv.Env.GetString("IDENTITY_SERVER_HOST", "localhost:5001").Replace("\"", "");
             var IdentityServerEndpoint = $"http://{IdentityDNS}";
             Log.Information("Connect to Identity Provider: " + IdentityServerEndpoint);
             options.RequireHttpsMetadata = false;
             // Clear default Microsoft's JWT claim mapping
             // Ref: https://stackoverflow.com/questions/70766577/asp-net-core-jwt-token-is-transformed-after-authentication
             options.MapInboundClaims = false;

             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateAudience = false,
                 ValidateIssuer = false,
                 RoleClaimType = "role", // map the role claim to jwt

                 ValidateIssuerSigningKey = false,
                 SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                 {
                     var jwt = new JsonWebToken(token);

                     return jwt;
                 },
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
                     Console.WriteLine("------------------------");
                     Console.WriteLine(accessToken);
                     // If the request is for our hub...
                     var path = context.HttpContext.Request.Path;
                     if (!string.IsNullOrEmpty(accessToken) &&
                         path.StartsWithSegments(HUB_ENDPOINT))
                     {
                         // Read the token out of the query string
                         context.Token = accessToken;
                     }
                     return Task.CompletedTask;
                 }
             };
         });

        services.AddSignalR(options =>
        {
            //Global filter
            options.AddFilter<GlobalLoggingFilter>();
        })
            .AddNewtonsoftJsonProtocol(options =>
            {
                options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSPAClientOrigin",
                builder =>
                {
                    builder.WithOrigins(reactUrl)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
        });
        return builder;
    }

    public static WebApplication UseChatHubService(this WebApplication app)
    {
        // Set endpoint for a chat hub
        app.UseSerilogRequestLogging();
        app.UseCors("AllowSPAClientOrigin");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapHub<HubServer>(HUB_ENDPOINT);
        return app;
    }
}
