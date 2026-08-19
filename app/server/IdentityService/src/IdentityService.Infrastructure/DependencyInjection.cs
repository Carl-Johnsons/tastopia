using Contract.Extension;
using Contract.Middleware;
using Contract.Utilities;
using IdentityService.Infrastructure.Persistence;
using IdentityService.Infrastructure.Persistence.Mockup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        => services.AddPersistence()
                    .AddInternalInfrastructureServices()
                    .AddExternalInfrastructureServices();

    public static IServiceCollection AddMinimalInfrastructureServices(this IServiceCollection services)
    {
        var connectionString = EnvUtility.GetConnectionString();

        services.AddPersistence()
                .AddIdentityServer()
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(connectionString,
                                            options => options.MigrationsAssembly("IdentityService.Infrastructure")
                                                                .EnableRetryOnFailure(
                                                                    maxRetryCount: 10,
                                                                    maxRetryDelay: TimeSpan.FromSeconds(15),
                                                                    errorCodesToAdd: null
                                                                ));

                });
        services.AddIdentity<ApplicationAccount, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        services.AddScoped<MockupData>();
        services.AddLogging();
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        var connectionString = EnvUtility.GetConnectionString();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
        return services;
    }

    private static IServiceCollection AddInternalInfrastructureServices(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationAccount, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        // MediatR require repository scope dependency injection
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped<MockupData>();
        services.AddScoped(typeof(IPaginateDataUtility<,>), typeof(PaginateDataUtility<,>));

        return services;
    }

    private static IServiceCollection AddExternalInfrastructureServices(this IServiceCollection services)
    {
        services.AddServiceDiscoveryService()
                .AddMessagingService("DuendeIdentityServer")
                .AddSignalRService();
        return services;
    }

    public static WebApplication UseInfrastructureServices(this WebApplication app)
        => app.UseLoggingServices()
               .UseServiceDiscoveryService(DotNetEnv.Env.GetString("CONSUL_IDENTITY", "Not Found"));
}
