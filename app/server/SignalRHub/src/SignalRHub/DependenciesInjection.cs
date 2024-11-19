using Contract.Event.ConversationEvent;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using SignalRHub.Filters;
using SignalRHub.Hubs;

namespace SignalRHub;

public static class DependenciesInjection
{
    public static WebApplicationBuilder AddChatHubServices(this WebApplicationBuilder builder)
    {
        DotNetEnv.Env.Load();

        var reactUrl = Environment.GetEnvironmentVariable("REACT_URL") ?? "http://localhost:3000";

        var services = builder.Services;
        var host = builder.Host;

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
             var IdentityDNS = (Environment.GetEnvironmentVariable("IDENTITY_SERVER_HOST") ?? "localhost:5001").Replace("\"", "");
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
                         path.StartsWithSegments("/chat-hub"))
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

        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();

            busConfig.UsingRabbitMq((context, config) =>
            {
                var username = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER") ?? "admin";
                var password = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS") ?? "pass";
                var rabbitMQHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost:5672";

                config.Host(new Uri($"amqp://{rabbitMQHost}/"), h =>
                {
                    h.Username(username);
                    h.Password(password);
                });

                config.ConfigureEndpoints(context);
            });
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
        app.MapHub<HubServer>("/chat-hub");
        return app;
    }
}
