using Contract.Extension;
using Contract.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using TrackingService.Infrastructure.Persistence;
using TrackingService.Infrastructure.Persistence.Mockup;

namespace TrackingService.Infrastructure;

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
                .AddMessagingService("TrackingService.API");
        return services;
    }

    public static WebApplication UseInfrastructureServices(this WebApplication app)
        => app.UseLoggingServices()
               .UseServiceDiscoveryService(DotNetEnv.Env.GetString("CONSUL_TRACKING", "Not Found"));

}
