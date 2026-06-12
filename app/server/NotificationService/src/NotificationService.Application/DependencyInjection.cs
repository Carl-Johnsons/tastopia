using AutoMapper;
using Grpc.Core;
using NotificationService.Application.Configs;
using RecipeProto;
using UserProto;

namespace NotificationService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register automapper
        services.AddAutoMapper(
            cfg =>
            {
                cfg.LicenseKey = DotNetEnv.Env.GetString("LUCKYPENNYSOFTWARE_LICENSE_KEY", "Not Found");
            },
            AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
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

        services.AddGrpcClient<GrpcUser.GrpcUserClient>(options =>
        {
            options.Address = consulService.GetGrpcServiceUri(DotNetEnv.Env.GetString("CONSUL_USER", "Not Found"));
        }).ConfigureChannel(channelOptions);

        services.AddGrpcClient<GrpcRecipe.GrpcRecipeClient>(options =>
        {
            options.Address = consulService.GetGrpcServiceUri(DotNetEnv.Env.GetString("CONSUL_RECIPE", "Not Found"));
        }).ConfigureChannel(channelOptions);
    }
}
