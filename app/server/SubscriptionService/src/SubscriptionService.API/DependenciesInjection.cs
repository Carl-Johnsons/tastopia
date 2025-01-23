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
using Serilog;

namespace SubscriptionService.API;

public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();
        var services = builder.Services;
        var config = builder.Configuration;
        var host = builder.Host;

        builder.ConfigureSerilog();
        builder.ConfigureKestrel();

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
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                // For development only
                options.IncludeErrorDetails = true;
            });

        return builder;
    }

    public static WebApplication UseAPIServices(this WebApplication app)
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

