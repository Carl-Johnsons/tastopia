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

    public static async Task<WebApplication> UseAPIServicesAsync(this WebApplication app)
    {
        app.UseCommonServices(DotNetEnv.Env.GetString("CONSUL_NOTIFICATION", "Not Found"));
        app.UseSwaggerServices();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseGrpcServices();

        app.UseAuthentication();
        app.UseAuthorization();

        await app.UseSignalRServiceAsync();
        return app;
    }
}

