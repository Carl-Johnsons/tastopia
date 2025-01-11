using RecipeService.API.GrpcServices;

namespace RecipeService.API.Extensions;

public static class GrpcExtension
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddGrpc();
        return services;
    }

    public static WebApplication UseGrpcServices(this WebApplication app)
    {
        app.UseRouting();
        app.MapGrpcService<GrpcRecipeService>();
        return app;
    }
}