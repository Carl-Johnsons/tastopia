using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Contract.Utilities;
using APIGateway.Extensions;
using Ocelot.Provider.Consul;
using Contract.Extension;

namespace APIGateway;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();
        var reactUrl = DotNetEnv.Env.GetString("REACT_URL", "http://localhost:3000");
        var config = builder.Configuration;
        var services = builder.Services;
        var host = builder.Host;
        var env = builder.Environment;

        builder.ConfigureCommonAPIServices();

        services.AddConsulRegistryService();

        config.SetBasePath(env.ContentRootPath)
              .AddEnvironmentVariables();

        if (env.IsDevelopment())
        {
            config.AddOcelot("Config/development", env);
        }
        else if (env.IsEnvironment("Kubernetes"))
        {
            config.AddOcelot("Config/kubernetes", env);
        }
        else
        {
            config.AddOcelot("Config/production", env);
        }

        services.AddLogging(options =>
        {
            options.AddConsole();
        });

        // Configure service

        services.AddOcelot()
                .AddConsul();

        services.AddSwaggerServices(config);

        services.AddAPIGatewayAPIServices();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOriginPolicy",
                builder =>
                {
                    builder.WithOrigins(reactUrl)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
        });
        services.AddSignalR();
        return builder;
    }

    public static WebApplication UseAPIServices(this WebApplication app)
    {
        app.UseSerilogServices();

        app.UseCors("AllowAnyOriginPolicy");

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseRouting();
        app.UseHealthCheck();
#pragma warning disable ASP0014 // Suggest using top level route registrations
        app.UseEndpoints(e =>
        {
            e.MapControllers();
        });
#pragma warning restore ASP0014 // Suggest using top level route registrations

        app.UseOcelot().Wait();

        app.UseConsulServiceDiscovery(DotNetEnv.Env.GetString("CONSUL_API_GATEWAY", "Not Found"));
        return app;
    }
}

