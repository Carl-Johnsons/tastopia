using Contract.Extension;
using Contract.Utilities;
using RecipeService.API.Extensions;
using RecipeService.Application;
using RecipeService.Infrastructure;

namespace RecipeService.API;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();
        var services = builder.Services;
        var databaseName = DotNetEnv.Env.GetString("DB");

        builder.ConfigureLoggingService()
               .ConfigureKestrel()
               .ConfigureLivenessCheck()
               .ConfigureMongoDBHealthCheck(databaseName);

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

        services.AddCommonAPIServices()
                .AddCustomDownstreamAuthentication();

        return builder;
    }

    public static async Task<WebApplication> UseAPIServicesAsync(this WebApplication app)
    {
        app.UseInfrastructureServices()
           .UseSwaggerServices()
           .UseCommonAPIMiddleware();

        app.UseRouting();
        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseGrpcServices()
           .UseCustomHealthCheck();

        await app.UseSignalRServiceAsync();

        return app;
    }
}

