using Contract.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace SignalRService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddCommonInfrastructureServices("SignalRHub");
        return services;
    }
}
