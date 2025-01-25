using AutoMapper;
using Contract.Extension;
using Contract.Utilities;
using UploadFileService.API.Configs;
using UploadFileService.API.Extensions;
using UploadFileService.API.Middleware;
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
        var config = builder.Configuration;
        var host = builder.Host;

        builder.ConfigureCommonAPIServices();

        services.AddInfrastructureServices();
        services.AddApplicationServices();
        services.AddGrpcServices();
        services.AddSwaggerServices();

        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddCommonAPIServices();

        return builder;
    }

    public static WebApplication UseAPIServices(this WebApplication app)
    {
        app.UseSerilogServices();
        app.UseConsulServiceDiscovery(DotNetEnv.Env.GetString("CONSUL_UPLOAD", "Not Found"));
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

