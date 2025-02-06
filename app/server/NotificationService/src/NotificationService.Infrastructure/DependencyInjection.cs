
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Contract.Extension;

namespace NotificationService.Infrastructure;

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
        services.AddCommonInfrastructureServices("NotificationService.API");

        using (var serviceProvider = services.BuildServiceProvider())
        {
            var mockupData = serviceProvider.GetRequiredService<MockupData>();
            mockupData.SeedAllData().Wait();
        }

        return services;
    }
}
