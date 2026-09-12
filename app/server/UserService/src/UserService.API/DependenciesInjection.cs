using Contract.Extension;
using Contract.Utilities;
using UserService.API.Extensions;
using UserService.Application;
using UserService.Infrastructure;

namespace UserService.API;

public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();
        var services = builder.Services;
        var config = builder.Configuration;
        var databaseName = DotNetEnv.Env.GetString("DB");

        builder.ConfigureLoggingService()
               .ConfigureKestrel()
               .ConfigureLivenessCheck()
               .ConfigurePostgresHealthCheck(databaseName);

        services.AddInfrastructureServices()
                .AddApplicationServices()
                .AddGrpcServices()
                .AddSwaggerServices();

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
           .UseSwaggerServices()
           .UseCommonAPIMiddleware();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.UseGrpcServices()
           .UseCustomHealthCheck();

        return app;
    }
}