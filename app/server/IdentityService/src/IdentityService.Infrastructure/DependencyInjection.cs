using Contract.Extension;
using IdentityService.Infrastructure.Persistence;
using IdentityService.Infrastructure.Persistence.Mockup;
using IdentityService.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();

        services.AddIdentity<ApplicationAccount, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


        // MediatR require repository scope dependency injection
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped<MockupData>();
        services.AddCommonInfrastructureServices("DuendeIdentityServer");
        services.AddSingleton<ISignalRService, SignalRService>();

        using (var serviceProvider = services.BuildServiceProvider())
        {
            var mockupData = serviceProvider.GetRequiredService<MockupData>();
            mockupData.SeedAllData().Wait();
        }

        return services;
    }
}
