namespace APIGateway.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer(); // This require for swaggerForOcelot to launch
        services.AddSwaggerForOcelot(config);
        return services;
    }

    public static WebApplication UseSwaggerServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerForOcelotUI();
        return app;
    }
}
