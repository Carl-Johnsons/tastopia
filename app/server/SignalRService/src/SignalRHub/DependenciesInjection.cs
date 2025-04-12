using Contract.Extension;
using Contract.Utilities;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SignalRHub.Extensions;
using SignalRHub.Filters;
using SignalRHub.Hubs;
using SignalRHub.Interfaces;
using SignalRHub.Services;

namespace SignalRHub;

public static class DependenciesInjection
{
    private static string HUB_ENDPOINT = "/tastopia-hub";
    public static WebApplicationBuilder AddChatHubServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();

        var services = builder.Services;
        var host = builder.Host;

        builder.ConfigureCommonAPIServices();

        services.AddCommonInfrastructureServices("SignalRHub");

        var apiGatewayUrl = DotNetEnv.Env.GetString("API_GATEWAY_URL", "https://localhost:7000");

        services.AddSingleton<IMemoryTracker, MemoryTracker>();

        services.AddHttpContextAccessor();

        services.AddCustomAuthentication(HUB_ENDPOINT);

        services.AddSignalR(options =>
        {
            //Global filter
            options.AddFilter<GlobalLoggingFilter>();
        })
        .AddNewtonsoftJsonProtocol(options =>
        {
            options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSPAClientOrigin",
                builder =>
                {
                    builder.WithOrigins(apiGatewayUrl)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
        });
        return builder;
    }

    public static WebApplication UseChatHubService(this WebApplication app)
    {
        // Set endpoint for a chat hub
        app.UseSerilogServices();
        app.UseConsulServiceDiscovery(DotNetEnv.Env.GetString("CONSUL_SIGNALR", "Not Found"), IsSecure: false);
        app.UseCors("AllowSPAClientOrigin");
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapHub<HubServer>(HUB_ENDPOINT);
        app.UseCustomHealthCheck();
        return app;
    }
}
