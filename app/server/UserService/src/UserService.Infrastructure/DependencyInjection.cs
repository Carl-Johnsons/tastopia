using Contract.Extension;
using Contract.Middleware;
using Contract.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Persistence.Mockup;

namespace UserService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        => services.AddPersistence()
                   .AddInternalInfrastructureServices()
                   .AddExternalInfrastructureServices();

    public static IServiceCollection AddMinimalInfrastructureServices(this IServiceCollection services)
    {
        services.AddPersistence();
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
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
        // MediatR require repository scope dependency injection
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped<MockupData>();
        services.AddScoped(typeof(IPaginateDataUtility<,>), typeof(PaginateDataUtility<,>));

        return services;
    }

    private static IServiceCollection AddExternalInfrastructureServices(this IServiceCollection services)
        => services.AddServiceDiscoveryService()
                    .AddMessagingService("UserService.API");

    public static WebApplication UseInfrastructureServices(this WebApplication app)
        => app.UseLoggingServices()
               .UseServiceDiscoveryService(DotNetEnv.Env.GetString("CONSUL_USER", "Not Found"));
}
