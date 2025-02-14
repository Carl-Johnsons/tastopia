using Consul;
using Contract.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Contract.Extension;

public static class ConsulExtension
{
    private static bool IsSecure = true;
    public static WebApplication UseConsulServiceDiscovery(this WebApplication app, string serviceName)
    {
        var consulClient = app.Services.GetRequiredService<IConsulClient>();
        var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

        var serviceHost = DotNetEnv.Env.GetString("SERVICE_HOST", "Not Found");
        var servicePort = DotNetEnv.Env.GetInt("HTTPS_PORT", 0);

        var random = new Random();
        var number = random.Next(1000000000, 2000000000);
        var serviceId = $"{serviceName}-{number}";
        var scheme = IsSecure ? "https" : "http";

        var healthCheckEndpoint = EnvUtility.IsDevelopment()
                            ? $"{scheme}://host.docker.internal:{servicePort}/health"
                            : $"{scheme}://{serviceHost}:{servicePort}/health";

        var registration = new AgentServiceRegistration()
        {
            ID = serviceId,
            Name = serviceName,
            Address = serviceHost,
            EnableTagOverride = true,
            Tags = ["secure=true"],
            Port = servicePort,
            Check = new AgentServiceCheck
            {
                Timeout = TimeSpan.FromSeconds(10),
                Interval = TimeSpan.FromSeconds(20),
                HTTP = healthCheckEndpoint,
                TLSSkipVerify = true,
            }
        };

        lifetime.ApplicationStarted.Register(async () =>
        {
            Log.Information("Registering to Consul");
            await consulClient.Agent.ServiceDeregister(registration.ID);
            await consulClient.Agent.ServiceRegister(registration);
        });


        lifetime.ApplicationStopping.Register(async () =>
        {
            Log.Information("Unregistering from Consul");
            await consulClient.Agent.ServiceDeregister(registration.ID);
        });
        return app;
    }
}