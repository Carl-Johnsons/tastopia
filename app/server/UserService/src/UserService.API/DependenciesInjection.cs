using UserService.API.Middleware;
using UserService.Application;
using UserService.Infrastructure;
using AutoMapper;
using UserService.API.Configs;
using Contract.Utilities;
using UserService.API.Extensions;
using Contract.Extension;

namespace UserService.API;

public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();
        var services = builder.Services;
        var config = builder.Configuration;
        var host = builder.Host;

        builder.ConfigureCommonAPIServices();

        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddInfrastructureServices();
        services.AddApplicationServices();
        services.AddGrpcServices();
        services.AddSwaggerServices();

        services.AddCommonAPIServices();

        return builder;
    }

    public static WebApplication UseAPIServices(this WebApplication app)
    {
        app.UseSerilogServices();
        app.UseConsulServiceDiscovery(DotNetEnv.Env.GetString("CONSUL_USER", "Not Found"));
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