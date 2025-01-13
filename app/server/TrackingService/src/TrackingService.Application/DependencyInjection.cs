using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RecipeProto;
using System.Reflection;
using TrackingService.Application.Configs;


namespace TrackingService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        //Grpc
        services.AddGrpcClientService();

        return services;
    }

    private static void AddGrpcClientService(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var consulService = serviceProvider.GetRequiredService<IConsulRegistryService>();

    
        services.AddGrpcClient<GrpcRecipe.GrpcRecipeClient>(options =>
        {
            options.Address = consulService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_RECIPE", "Not Found"));
        });

     
    }
}
