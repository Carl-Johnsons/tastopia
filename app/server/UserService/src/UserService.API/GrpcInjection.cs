using UserService.API.GrpcServices;

namespace UserService.API;

public static class GrpcInjection
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddGrpc();
        return services;
    }

    public static WebApplication UseGrpcServices(this WebApplication app)
    {
        app.UseRouting();
        app.MapGrpcService<GrpcUserService>();
        return app;
    }
}