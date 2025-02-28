using Contract.Interfaces;
using Contract.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Contract.Extension;

public static class SignalRExtension
{
    /// <summary>
    /// Require consul service registry in order to work
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IServiceCollection AddSignalRService(this IServiceCollection services)
    {
        services.AddSingleton<ISignalRService, SignalRService>();
        return services;
    }

    /**
     * <summary>
     * </summary>
     */
    public static async Task<WebApplication> UseSignalRServiceAsync(this WebApplication app)
    {
        try
        {
            var signalRService = app.Services.GetService<ISignalRService>();
            await signalRService!.StartConnectionAsync();
        }
        catch (Exception ex)
        {
            Log.Error($"Error connecting to SignalR: {ex.Message}");
        }
        return app;
    }
}
