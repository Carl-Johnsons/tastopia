using RecipeService.Application;
using RecipeService.Infrastructure;
using RecipeService.API.Middleware;
using AutoMapper;
using RecipeService.API.Configs;
using Contract.Utilities;
using RecipeService.API.Extensions;
using Contract.Extension;

namespace RecipeService.API;

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
        services.AddGrpcServices();
        services.AddSwaggerServices();

        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddCommonAPIServices();

        services.AddEndpointsApiExplorer();
        return builder;
    }

    public static WebApplication UseAPIServices(this WebApplication app)
    {
        app.UseSerilogServices();
        app.UseConsulServiceDiscovery(DotNetEnv.Env.GetString("CONSUL_RECIPE", "Not Found"));

        app.UseSwaggerServices();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseGrpcServices();

        app.UseGlobalHandlingErrorMiddleware();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseRouting();
        app.UseHealthCheck();

        //try
        //{
        //    var signalRService = app.Services.GetService<ISignalRService>();
        //    await signalRService!.StartConnectionAsync();
        //}
        //catch (Exception ex)
        //{
        //    app.Logger.LogError($"Error connecting to SignalR: {ex.Message}");
        //}
        return app;
    }
}

