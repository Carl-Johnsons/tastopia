namespace NotificationService.API.Extensions;

public static class GrpcExtension
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddGrpc();
        return services;
    }
}