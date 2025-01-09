using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using RecipeService.Application.Configs;
using System.Reflection;
using UploadFileProto;
using UserProto;

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
        services.AddGrpcClient<GrpcUser.GrpcUserClient>(options =>
        {
            options.Address = new Uri("https://localhost:7003");
        });
        services.AddGrpcClient<GrpcUploadFile.GrpcUploadFileClient>(options =>
        {
            options.Address = new Uri("https://localhost:7002");
        });
    }
}
