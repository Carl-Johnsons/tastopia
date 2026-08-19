using Contract.Extension;
using Contract.Utilities;
using UploadFileService.API.Extensions;
using UploadFileService.Application;
using UploadFileService.Infrastructure;

namespace UploadFileService.API;

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
                .AddGrpcServices()
                .AddSwaggerServices();

        // Register automapper
        services.AddAutoMapper(
            cfg =>
            {
                cfg.LicenseKey = DotNetEnv.Env.GetString("LUCKYPENNYSOFTWARE_LICENSE_KEY", "Not Found");
            },
            AppDomain.CurrentDomain.GetAssemblies());

        services.AddCommonAPIServices();

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

        app.UseGrpcServices()
           .UseCustomHealthCheck()
           .UseCommonAPIMiddleware();

        return app;
    }
}

