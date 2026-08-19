
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Contract.Extension;
using Contract.Utilities;
using Microsoft.AspNetCore.Builder;
using Contract.Middleware;

namespace NotificationService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        return services.AddPersistence()
                        .AddInternalInfrastructureServices()
                        .AddExternalInfrastructureServices();
    }

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
        // This line only use 1 in infrastructure
        if (!BsonClassMap.IsClassMapRegistered(typeof(Guid)))
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();
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
    {
        services.AddServiceDiscoveryService()
                .AddMessagingService("NotificationService.API")
                .AddSignalRService();
        return services;
    }

    public static WebApplication UseInfrastructureServices(this WebApplication app)
        => app.UseLoggingServices()
               .UseServiceDiscoveryService(DotNetEnv.Env.GetString("CONSUL_NOTIFICATION", "Not Found"));
}
