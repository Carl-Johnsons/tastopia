using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using AutoMapper;
using TrackingService.API.Configs;
using TrackingService.API.Middleware;
using TrackingService.Infrastructure;
using TrackingService.Application;
using Contract.Utilities;
using TrackingService.API.Extensions;
using Serilog;
using Contract.Extension;

namespace TrackingService.API;

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
        builder.ConfigureHealthCheck();

        services.AddInfrastructureServices();
        services.AddApplicationServices();
        services.AddErrorValidation();
        services.AddSwaggerServices();

        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddCommonAPIServices();

        services.AddEndpointsApiExplorer();

        return builder;
    }

    public static WebApplication UseAPIServices(this WebApplication app)
    {
        app.UseSerilogServices();
        app.UseConsulServiceDiscovery(DotNetEnv.Env.GetString("CONSUL_TRACKING", "Not Found"));

        app.UseSwaggerServices();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.UseGlobalHandlingErrorMiddleware();

        app.UseHealthCheck();

        return app;
    }
}

