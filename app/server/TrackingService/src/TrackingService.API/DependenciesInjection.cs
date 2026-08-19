using Contract.Extension;
using Contract.Utilities;
using TrackingService.API.Extensions;
using TrackingService.Application;
using TrackingService.Infrastructure;

namespace TrackingService.API;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();
        var services = builder.Services;

        builder.ConfigureLoggingService()
               .ConfigureKestrel()
               .ConfigureHealthCheck();

        services.AddInfrastructureServices()
                .AddApplicationServices()
                .AddSwaggerServices();

        // Register automapper
        services.AddAutoMapper(
            cfg =>
            {
                cfg.LicenseKey = DotNetEnv.Env.GetString("LUCKYPENNYSOFTWARE_LICENSE_KEY", "Not Found");
            },
            AppDomain.CurrentDomain.GetAssemblies());

        services.AddCommonAPIServices()
                .AddCustomDownstreamAuthentication();

        return builder;
    }

    public static WebApplication UseAPIServices(this WebApplication app)
    {
        app.UseInfrastructureServices()
           .UseSwaggerServices();

        // app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.UseCustomHealthCheck()
           .UseCommonAPIMiddleware();

        return app;
    }
}

