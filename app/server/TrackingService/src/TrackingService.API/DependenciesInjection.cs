using TrackingService.Infrastructure;
using TrackingService.Application;
using Contract.Utilities;
using TrackingService.API.Extensions;
using Contract.Extension;

namespace TrackingService.API;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();
        var services = builder.Services;

        builder.ConfigureCommonAPIServices();

        services.AddInfrastructureServices();
        services.AddApplicationServices();
        services.AddErrorValidation();
        services.AddSwaggerServices();

        // Register automapper
        services.AddAutoMapper(
            cfg =>
            {
                cfg.LicenseKey = DotNetEnv.Env.GetString("LUCKYPENNYSOFTWARE_LICENSE_KEY", "Not Found");
            },
            AppDomain.CurrentDomain.GetAssemblies());

        services.AddCommonAPIServices();

        services.AddEndpointsApiExplorer();

        return builder;
    }

    public static WebApplication UseAPIServices(this WebApplication app)
    {
        app.UseCommonServices(DotNetEnv.Env.GetString("CONSUL_TRACKING", "Not Found"));

        app.UseSwaggerServices();

        // app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}

