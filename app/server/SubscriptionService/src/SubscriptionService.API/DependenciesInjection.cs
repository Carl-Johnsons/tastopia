using SubscriptionService.Infrastructure;
using SubscriptionService.Application;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using AutoMapper;
using SubscriptionService.API.Configs;
using SubscriptionService.API.Middleware;
using SubscriptionService.Domain.Interfaces;
using Contract.Utilities;
using SubscriptionService.API.Extensions;

namespace SubscriptionService.API;

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

        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddInfrastructureServices();
        services.AddApplicationServices();
        services.AddSwaggerServices();
        services.AddGrpcServices();


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

        services.AddHttpContextAccessor();

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                var IdentityDNS = DotNetEnv.Env.GetString("IDENTITY_SERVER_HOST", "localhost:7001").Replace("\"", "");
                var IdentityServerEndpoint = $"https://{IdentityDNS}";
                Console.WriteLine("Connect to Identity Provider: " + IdentityServerEndpoint);

                options.Authority = IdentityServerEndpoint;
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
        app.UseSerilogServices();
        app.UseConsulServiceDiscovery();
        app.UseSwaggerServices();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseGrpcServices();

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

