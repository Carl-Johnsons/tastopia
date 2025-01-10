using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserProto;
using Steeltoe.Discovery.Client;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery;

namespace IdentityService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddGrpcClientServices();
        return services;
    }

    private static void AddGrpcClientServices(this IServiceCollection services)
    {
        services.AddHttpClient("UserService", o => o.BaseAddress = new Uri("https://user-service"))
                .AddServiceDiscovery()
                .AddRoundRobinLoadBalancer();

        services.AddGrpcClient<GrpcUser.GrpcUserClient>(options =>
        {
            var serviceProvider = services.BuildServiceProvider();
            var discoveryClient = serviceProvider.GetRequiredService<IDiscoveryClient>();
            var serviceInstance = discoveryClient.GetInstances("user-service").FirstOrDefault();

            if (serviceInstance != null)
            {
                options.Address = new Uri($"{serviceInstance.Uri}");
            }
            else
            {
                throw new Exception("User service not found via service discovery.");
            }
        });
    }
}
