using UserProto;

namespace NotificationService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddGrpcClientService();
        return services;
    }

    private static void AddGrpcClientService(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var consulService = serviceProvider.GetRequiredService<IConsulRegistryService>();

        services.AddGrpcClient<GrpcUser.GrpcUserClient>(options =>
        {
            options.Address = consulService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_USER", "Not Found"));
        });
    }
}
