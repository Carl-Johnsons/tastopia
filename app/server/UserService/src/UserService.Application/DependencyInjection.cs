using AccountProto;
using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UploadFileProto;
using UserService.Application.Configs;

namespace UserService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        // Register automapper
        services.AddAutoMapper(
            cfg =>
            {
                cfg.LicenseKey = DotNetEnv.Env.GetString("LUCKYPENNYSOFTWARE_LICENSE_KEY", "Not Found");
            },
            AppDomain.CurrentDomain.GetAssemblies());
        services.AddGrpcClientService();
        return services;
    }
    private static void AddGrpcClientService(this IServiceCollection services)
    {
        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        var serviceProvider = services.BuildServiceProvider();
        var consulService = serviceProvider.GetRequiredService<IConsulRegistryService>();

        Action<Grpc.Net.Client.GrpcChannelOptions> channelOptions = options =>
        {
            options.Credentials = ChannelCredentials.Insecure;
        };

        services.AddGrpcClient<GrpcAccount.GrpcAccountClient>(options =>
        {
            options.Address = consulService.GetGrpcServiceUri(DotNetEnv.Env.GetString("CONSUL_IDENTITY", "Not Found"));
        }).ConfigureChannel(channelOptions);

        services.AddGrpcClient<GrpcUploadFile.GrpcUploadFileClient>(options =>
        {
            options.Address = consulService.GetGrpcServiceUri(DotNetEnv.Env.GetString("CONSUL_UPLOAD", "Not Found"));
        }).ConfigureChannel(channelOptions);
    }
}
