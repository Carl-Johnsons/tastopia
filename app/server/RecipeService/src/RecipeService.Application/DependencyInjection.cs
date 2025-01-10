using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using RecipeService.Application.Configs;
using System.Reflection;
using UploadFileProto;
using UserProto;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery;

namespace RecipeService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(Guid)))
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        //Grpc
        services.AddGrpcClientService();

        return services;
    }
    private static void AddGrpcClientService(this IServiceCollection services)
    {
        services.AddHttpClient("UserService", o => o.BaseAddress = new Uri("https://user-service"))
            .AddServiceDiscovery()
            .AddRoundRobinLoadBalancer();

        services.AddHttpClient("UploadService", o => o.BaseAddress = new Uri("https://upload-service"))
            .AddServiceDiscovery()
            .AddRoundRobinLoadBalancer();

        services.AddGrpcClient<GrpcUser.GrpcUserClient>(options =>
        {
            var serviceProvider = services.BuildServiceProvider();
            var discoveryClient = serviceProvider.GetRequiredService<IDiscoveryClient>();
            var serviceInstance = discoveryClient.GetInstances("user-service").FirstOrDefault();

            if (serviceInstance != null)
            {
                options.Address = new Uri($"{serviceInstance.Uri}");
            }
            else
            {
                throw new Exception("User service not found via service discovery.");
            }
        });

        services.AddGrpcClient<GrpcUploadFile.GrpcUploadFileClient>(options =>
        {
            var serviceProvider = services.BuildServiceProvider();
            var discoveryClient = serviceProvider.GetRequiredService<IDiscoveryClient>();
            var serviceInstance = discoveryClient.GetInstances("upload-service").FirstOrDefault();

            if (serviceInstance != null)
            {
                options.Address = new Uri($"{serviceInstance.Uri}");
            }
            else
            {
                throw new Exception("User service not found via service discovery.");
            }
        });
    }
}
