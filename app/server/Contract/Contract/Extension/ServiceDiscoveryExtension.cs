using Consul;
using Contract.Interfaces;
using Contract.Services;
using Contract.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Contract.Extension;

public static class ServiceDiscoveryExtension
{
    /**
     * <summary>
     *   Add Service discovery by using Consul under the hood, only usable for api gateway and signalR service
     * </summary>
     */
    public static IServiceCollection AddServiceDiscoveryService(this IServiceCollection services)
    {
        services.AddSingleton<IConsulClient, ConsulClient>(serviceProvider =>
        {
            return new ConsulClient(config =>
            {
                var scheme = DotNetEnv.Env.GetString("CONSUL_SCHEME", "Not found");
                var host = DotNetEnv.Env.GetString("CONSUL_HOST", "Not found");
                var port = DotNetEnv.Env.GetString("CONSUL_PORT", "Not found");
                config.Address = new Uri($"{scheme}://{host}:{port}");
            });
        });
        services.AddSingleton<IConsulRegistryService, ConsulRegistryService>();
        return services;
    }

    public static WebApplication UseServiceDiscoveryService(this WebApplication app, string serviceName, bool IsSecure = true)
    {

        var consulClient = app.Services.GetRequiredService<IConsulClient>();
        var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

        var serviceHost = DotNetEnv.Env.GetString("SERVICE_HOST", "Not Found");
        var httpPort = DotNetEnv.Env.GetInt("PORT", 0);
        var httpsPort = DotNetEnv.Env.GetInt("HTTPS_PORT", 0);
        // Not need secure from this point
        IsSecure = false;

        var random = new Random();
        var number = random.Next(1000000000, 2000000000);
        var serviceId = $"{serviceName}-{number}";
        var scheme = IsSecure ? "https" : "http";

        var healthCheckEndpoint = EnvUtility.IsDevelopment()
                            ? $"{scheme}://host.docker.internal:{httpPort}/health"
                            : $"{scheme}://{serviceHost}:{httpPort}/health";

        var registration = new AgentServiceRegistration()
        {
            ID = serviceId,
            Name = serviceName,
            Address = serviceHost,
            EnableTagOverride = true,
            Tags = IsSecure ? ["secure=true"] : [],
            Port = httpPort,
            Meta = new Dictionary<string, string>
            {
                {"grpc_scheme", "http"},
                {"grpc_port", httpsPort + ""}
            },
            Check = new AgentServiceCheck
            {
                Timeout = TimeSpan.FromSeconds(10),
                Interval = TimeSpan.FromSeconds(20),
                HTTP = healthCheckEndpoint,
                TLSSkipVerify = true,
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1)
            }
        };

        lifetime.ApplicationStarted.Register(async () =>
        {
            Log.Information("Registering to Consul");
            await consulClient.Agent.ServiceDeregister(registration.ID);
            await consulClient.Agent.ServiceRegister(registration);
        });

        // Graceful shutdown handling
        lifetime.ApplicationStopping.Register(async () =>
        {
            Log.Information("Unregistering from Consul");
            await consulClient.Agent.ServiceDeregister(registration.ID);
        });
        // Handle SIGINT (Ctrl+C) and SIGTERM
        AppDomain.CurrentDomain.ProcessExit += async (sender, eventArgs) =>
        {
            Log.Information("Application is exiting. Deregistering from Consul...");
            await consulClient.Agent.ServiceDeregister(serviceId);
        };

        return app;
    }
}