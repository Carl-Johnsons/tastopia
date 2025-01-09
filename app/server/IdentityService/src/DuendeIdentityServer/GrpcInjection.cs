using DuendeIdentityServer.GrpcServices;

namespace DuendeIdentityServer;

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
        app.MapGrpcService<GrpcAccountService>();
        return app;
    }
}