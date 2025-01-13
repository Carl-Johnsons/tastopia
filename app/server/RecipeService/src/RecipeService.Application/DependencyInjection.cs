using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RecipeService.Application.Configs;
using System.Reflection;
using UploadFileProto;
using UserProto;
using TrackingProto;
using Contract.Constants;
using Grpc.Net.Compression;
using System.IO.Compression;

namespace RecipeService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
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
        var serviceProvider = services.BuildServiceProvider();
        var consulService = serviceProvider.GetRequiredService<IConsulRegistryService>();

        services.AddGrpcClient<GrpcUser.GrpcUserClient>(options =>
        {
            options.Address = consulService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_USER", "Not Found"));
        });

        services.AddGrpcClient<GrpcUploadFile.GrpcUploadFileClient>(options =>
        {
            options.Address = consulService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_UPLOAD", "Not Found"));

        }).ConfigureChannel(options =>
        {
            options.MaxReceiveMessageSize = GrpcUploadFileConfig.MaxMessageSize;
            options.MaxSendMessageSize = GrpcUploadFileConfig.MaxMessageSize;
            options.CompressionProviders = new[] { new GzipCompressionProvider(CompressionLevel.Optimal) };
        });

        services.AddGrpcClient<GrpcTracking.GrpcTrackingClient>(options =>
        {
            options.Address = consulService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_TRACKING", "Not Found"));
        });
    }
}
