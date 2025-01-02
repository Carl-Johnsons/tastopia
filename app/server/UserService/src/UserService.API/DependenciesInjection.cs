using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Serilog;
using UserService.API.Middleware;
using UserService.Application;
using UserService.Infrastructure;
using Newtonsoft.Json;
using AutoMapper;
using UserService.API.Configs;
using Microsoft.OpenApi.Models;
using UserService.Infrastructure.Utilities;
using UserService.Domain.Interfaces;

namespace UserService.API;

public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();
        var services = builder.Services;
        var config = builder.Configuration;
        var host = builder.Host;

        host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddApplicationServices();
        services.AddInfrastructureServices(config);

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Prevent circular JSON references
                options.JsonSerializerOptions.WriteIndented = true; // Pretty-print JSON
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Error; // Error on missing members
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            var IdentityDNS = DotNetEnv.Env.GetString("IDENTITY_SERVER_HOST", "localhost:5001").Replace("\"", "");
            var IdentityServerEndpoint = $"http://{IdentityDNS}";

            c.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri($"{IdentityServerEndpoint}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "Required to sign in" },
                            { "profile", "Get the profile of the user" },
                            { "phone", "Get phone claim" },
                            { "email", "Get email claim" },
                            { "offline_access", "Required for refresh token" },
                            { "IdentityServerApi", "Required for access to identity api" },
                        }
                    }
                },
                Description = "OAuth2 Password Grant"
            });
        });

        services.AddHttpContextAccessor();

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                var IdentityDNS = DotNetEnv.Env.GetString("IDENTITY_SERVER_HOST", "localhost:5001").Replace("\"", "");
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

        app.UseSwagger();
        app.UseSwaggerUI();

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

