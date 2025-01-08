using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RecipeService.Application.Configs;
using System.Reflection;
using UserProto;

namespace RecipeService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddGrpcClientService();
        return services;
    }
    private static void AddGrpcClientService(this IServiceCollection services)
    {
        services.AddGrpcClient<GrpcUser.GrpcUserClient>(options =>
        {
            options.Address = new Uri("https://localhost:7003");
        });
    }
}
