using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserProto;

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
        services.AddGrpcClient<GrpcUser.GrpcUserClient>(options =>
        {
            options.Address = new Uri("https://localhost:7003");
        });
    }
}
