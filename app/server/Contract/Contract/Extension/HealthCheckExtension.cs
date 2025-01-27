using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Contract.Extension;

public static class HealthCheckExtension
{
    public static WebApplicationBuilder ConfigureHealthCheck(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();
        return builder;
    }

    /**
     * <summary>
     *  In order to use the health check UseHealthCheck after UseRouting
     * </summary>
     */
    public static WebApplication UseHealthCheck(this WebApplication app)
    {
        app.MapHealthChecks("/health");
        return app;
    }
}
