using AccountProto;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Steeltoe.Discovery.Client;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery;
using UserService.Application.Configs;

namespace UserService.Application;

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
        {
            services.AddHttpClient("IdentityService", o => o.BaseAddress = new Uri("https://identity-service"))
                    .AddServiceDiscovery()
                    .AddRoundRobinLoadBalancer();

            services.AddGrpcClient<GrpcAccount.GrpcAccountClient>(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var discoveryClient = serviceProvider.GetRequiredService<IDiscoveryClient>();
                var serviceInstance = discoveryClient.GetInstances("identity-service").FirstOrDefault();

                if (serviceInstance != null)
                {
                    options.Address = new Uri($"{serviceInstance.Uri}");
                }
                else
                {
                    throw new Exception("Identity service not found via service discovery.");
                }
            });

        }
    }
}
