using Contract.Extension;
using Contract.Utilities;
using NotificationService.API.Extensions;
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

        builder.ConfigureLoggingService()
               .ConfigureKestrel()
               .ConfigureHealthCheck();

        services.AddInfrastructureServices()
                .AddApplicationServices()
                .AddGrpcServices()
                .AddSwaggerServices();

        services.AddCommonAPIServices()
                .AddCustomDownstreamAuthentication();

        return builder;
    }

    public static async Task<WebApplication> UseAPIServicesAsync(this WebApplication app)
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

        await app.UseSignalRServiceAsync();
        return app;
    }
}

