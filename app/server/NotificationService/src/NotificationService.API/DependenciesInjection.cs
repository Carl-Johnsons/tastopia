using Contract.Extension;
using Contract.Utilities;
using NotificationService.API.Extensions;
using NotificationService.API.Middleware;
using NotificationService.Application;
using NotificationService.Infrastructure;

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

        builder.ConfigureCommonAPIServices();

        services.AddInfrastructureServices();
        services.AddApplicationServices();
        services.AddGrpcServices();
        services.AddSwaggerServices();

        services.AddCommonAPIServices();

        services.AddEndpointsApiExplorer();

        return builder;
    }

    public static WebApplication UseAPIServices(this WebApplication app)
    {
        app.UseSerilogServices();
        app.UseConsulServiceDiscovery(DotNetEnv.Env.GetString("CONSUL_NOTIFICATION", "Not Found"));
        app.UseSwaggerServices();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseGrpcServices();

        app.UseGlobalHandlingErrorMiddleware();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseRouting();
        app.UseHealthCheck();

        return app;
    }
}

