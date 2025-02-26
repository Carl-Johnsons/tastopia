using Contract.Extension;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionService.Infrastructure.Persistence;
using SubscriptionService.Infrastructure.Persistence.Mockup;

namespace SubscriptionService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();

        // MediatR require repository scope dependency injection
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped<MockupData>();
        services.AddCommonInfrastructureServices("SubscriptionService.API");

        using (var serviceProvider = services.BuildServiceProvider())
        {
            var mockupData = serviceProvider.GetRequiredService<MockupData>();
            mockupData.SeedDataAsync().Wait();
        }

        return services;
    }
}
