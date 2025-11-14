using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using TrackingService.Infrastructure.Persistence;
using TrackingService.Infrastructure.Persistence.Mockup;
using Contract.Extension;

namespace TrackingService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // This line only use 1 in infrastructure
        if (!BsonClassMap.IsClassMapRegistered(typeof(Guid)))
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();

        // MediatR require repository scope dependency injection
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped<MockupData>();
        services.AddCommonInfrastructureServices("TrackingService.API");

        return services;
    }

    public static IServiceCollection AddMinimalInfrastructureServices(this IServiceCollection services)
    {
        // This line only use 1 in infrastructure
        if (!BsonClassMap.IsClassMapRegistered(typeof(Guid)))
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped<MockupData>();
        services.AddLogging();
        return services;
    }
}
