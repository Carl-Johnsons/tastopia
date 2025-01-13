using Consul;
using Contract.Utilities;

namespace UploadFileService.API.Extensions;

public static class ConsulExtension
{
    public static WebApplication UseConsulServiceDiscovery(this WebApplication app)
    {
        app.MapGet("/health", () =>
        {
            return Results.Ok();
        });

        var consulClient = app.Services.GetRequiredService<IConsulClient>();
        var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

        var serviceHost = DotNetEnv.Env.GetString("SERVICE_HOST", "Not Found");
        var servicePort = DotNetEnv.Env.GetInt("HTTPS_PORT", 0);
        var serviceName = DotNetEnv.Env.GetString("CONSUL_UPLOAD", "Not found");

        var random = new Random();
        var number = random.Next(1000000000, 2000000000);
        var serviceId = $"{serviceName}-{number}";

        var healthCheckEndpoint = EnvUtility.IsDevelopment()
                            ? $"https://host.docker.internal:{servicePort}/health"
                            : $"https://{serviceHost}:{servicePort}/health";
        // var uri = new Uri(address);
        // Better approach should be used to set the below settings. I hard coded just to explain.
        var registration = new AgentServiceRegistration()
        {
            ID = serviceId,
            Name = serviceName,
            Address = serviceHost,
            Tags = ["secure=true"], // This tells Consul that this service uses HTTPS
            EnableTagOverride = true,
            Port = servicePort,
            Check = new AgentServiceCheck
            {
                Timeout = TimeSpan.FromSeconds(10),
                Interval = TimeSpan.FromSeconds(20),
                HTTP = healthCheckEndpoint,
                TLSSkipVerify = true,
            }
        };

        lifetime.ApplicationStarted.Register(() =>
        {
            Console.WriteLine("Registering to Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);
        });


        lifetime.ApplicationStopping.Register(() =>
        {
            Console.WriteLine("Unregistering from Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
        });
        return app;
    }
}