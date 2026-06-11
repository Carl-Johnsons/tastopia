using Contract.Extension;
using IdentityService.Infrastructure.Persistence;
using IdentityService.Infrastructure.Persistence.Mockup;
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
        services.AddSignalRService();

        return services;
    }

    public static IServiceCollection AddMinimalInfrastructureServices(this IServiceCollection services)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                options.UseNpgsql(Contract.Utilities.EnvUtility.GetConnectionString()));
        services.AddIdentityServer()
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseNpgsql(
                            Contract.Utilities.EnvUtility.GetConnectionString(),
                            sql => sql.MigrationsAssembly("IdentityService.Infrastructure"));
                });
        services.AddIdentity<ApplicationAccount, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        services.AddScoped<MockupData>();
        services.AddLogging();
        return services;
    }
}
