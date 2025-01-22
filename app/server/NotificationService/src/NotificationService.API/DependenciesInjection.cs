using Contract.Utilities;
using Microsoft.IdentityModel.Tokens;
using NotificationService.API.Extensions;
using NotificationService.API.Middleware;
using NotificationService.Application;
using NotificationService.Domain.Interfaces;
using NotificationService.Infrastructure;
using Serilog;
using System.Text.Json.Serialization;

namespace NotificationService.API;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();
        var services = builder.Services;
        var config = builder.Configuration;
        var host = builder.Host;

        builder.ConfigureKestrel();
        builder.ConfigureSerilog();

        services.AddInfrastructureServices();
        services.AddApplicationServices();
        services.AddErrorValidation();
        services.AddGrpcServices();
        services.AddSwaggerServices();

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
                var serviceProvider = services.BuildServiceProvider();
                var consulRegistryService = serviceProvider.GetRequiredService<IConsulRegistryService>();
                var identityUri = consulRegistryService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_IDENTITY", "Not found"));
                Log.Information("Connect to Identity Provider: " + identityUri!.ToString());

                options.Authority = identityUri!.ToString();
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
        app.UseSerilogServices();
        app.UseConsulServiceDiscovery();
        app.UseSwaggerServices();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseGrpcServices();

        app.UseGlobalHandlingErrorMiddleware();

        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}

