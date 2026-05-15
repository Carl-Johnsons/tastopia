using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using RecipeProto;
using System.Reflection;
using TrackingService.Application.Configs;
using UserProto;


namespace TrackingService.Application;

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
        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        var serviceProvider = services.BuildServiceProvider();
        var consulService = serviceProvider.GetRequiredService<IConsulRegistryService>();

        Action<Grpc.Net.Client.GrpcChannelOptions> channelOptions = options =>
        {
            options.Credentials = ChannelCredentials.Insecure;
        };

        services.AddGrpcClient<GrpcRecipe.GrpcRecipeClient>(options =>
        {
            options.Address = consulService.GetGrpcServiceUri(DotNetEnv.Env.GetString("CONSUL_RECIPE", "Not Found"));
        }).ConfigureChannel(channelOptions);

        services.AddGrpcClient<GrpcUser.GrpcUserClient>(options =>
        {
            options.Address = consulService.GetGrpcServiceUri(DotNetEnv.Env.GetString("CONSUL_USER", "Not Found"));
        }).ConfigureChannel(channelOptions);
    }
}
