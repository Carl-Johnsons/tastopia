using AutoMapper;
using IdentityService.Application.Configs;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UploadFileProto;
using UserProto;

namespace IdentityService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddGrpcClientServices();
        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }

    private static void AddGrpcClientServices(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var consulService = serviceProvider.GetRequiredService<IConsulRegistryService>();

        services.AddGrpcClient<GrpcUser.GrpcUserClient>(options =>
        {
            options.Address = consulService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_USER", "Not Found"));
        });

        services.AddGrpcClient<GrpcUploadFile.GrpcUploadFileClient>(options =>
        {
            options.Address = consulService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_UPLOAD", "Not Found"));
        });
    }
}
