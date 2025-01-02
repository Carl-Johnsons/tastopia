using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Serilog;
using RecipeService.Application;
using RecipeService.Infrastructure;
using RecipeService.API.Middleware;
using Newtonsoft.Json;
using AutoMapper;
using RecipeService.API.Configs;

namespace RecipeService.API;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        //RecipeService.Infrastructure.Utilities.EnvUtility.LoadEnvFile();
        var services = builder.Services;
        var config = builder.Configuration;
        var host = builder.Host;

        host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

        services.AddApplicationServices();
        services.AddInfrastructureServices(config);

        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddControllers()
                // Prevent circular JSON reach max depth of the object when serialization
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                }).AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;
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

            // Apply the security scheme globally
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "OAuth2"
                        }
                    },
                    new List<string> { "openid", "profile", "phone", "email", "offline_access", "IdentityServerApi" }
                }
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

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseGlobalHandlingErrorMiddleware();

        app.UseAuthentication();

        app.UseAuthorization();

        //try
        //{
        //    var signalRService = app.Services.GetService<ISignalRService>();
        //    await signalRService!.StartConnectionAsync();
        //}
        //catch (Exception ex)
        //{
        //    app.Logger.LogError($"Error connecting to SignalR: {ex.Message}");
        //}
        return app;
    }
}

