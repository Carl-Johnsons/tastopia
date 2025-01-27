using Contract.Extension;
using Microsoft.Extensions.DependencyInjection;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Persistence.Mockup;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();

        // MediatR require repository scope dependency injection
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped<MockupData>();
        services.AddCommonInfrastructureServices("UserService.API");
        services.AddSingleton<ISignalRService, SignalRService>();

        using (var serviceProvider = services.BuildServiceProvider())
        {
            var mockupData = serviceProvider.GetRequiredService<MockupData>();
            mockupData.SeedDataAsync().Wait();
        }

        return services;
    }
}
