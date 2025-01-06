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
        services.AddSwaggerGen(config =>
        {
            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Input your Bearer token in the following format: `Bearer {your_token}`"
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
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
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
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
        app.UseSwaggerUI(c =>
        {
            c.ConfigObject.PersistAuthorization = true;
            c.InjectJavascript("/Swagger/inject-access-token.js");
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

