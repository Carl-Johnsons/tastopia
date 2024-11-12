using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Serilog;
using UserService.API.Middleware;
using UserService.Application;
using UserService.Infrastructure;

namespace UserService.API;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var config = builder.Configuration;
        var host = builder.Host;

        host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

        services.AddApplicationServices();
        services.AddInfrastructureServices(config);

        services.AddControllers()
                // Prevent circular JSON reach max depth of the object when serialization
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

        services.AddHttpContextAccessor();

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                var IdentityDNS = (Environment.GetEnvironmentVariable("IDENTITY_SERVER_HOST") ?? "localhost:5001").Replace("\"", "");
                var IdentityServerEndpoint = $"http://{IdentityDNS}";
                Console.WriteLine("Connect to Identity Provider: " + IdentityServerEndpoint);

                options.Authority = IdentityServerEndpoint;
                options.RequireHttpsMetadata = false;
                // Clear default Microsoft's JWT claim mapping
                // Ref: https://stackoverflow.com/questions/70766577/asp-net-core-jwt-token-is-transformed-after-authentication
                options.MapInboundClaims = false;

                options.TokenValidationParameters.ValidTypes = ["at+jwt"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
                // For development only
                options.IncludeErrorDetails = true;
            });

        services.AddEndpointsApiExplorer();

        return builder;
    }

    public static async Task<WebApplication> UseAPIServicesAsync(this WebApplication app)
    {

        app.Use(async (context, next) =>
        {
            // Log information about the incoming request
            app.Logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

            await next(); // Call the next middleware
        });

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseGlobalHandlingErrorMiddleware();

        app.UseAuthentication();

        app.UseAuthorization();

        try
        {
            var signalRService = app.Services.GetService<ISignalRService>();
            await signalRService!.StartConnectionAsync();
        }
        catch (Exception ex)
        {
            app.Logger.LogError($"Error connecting to SignalR: {ex.Message}");
        }
        return app;
    }
}

