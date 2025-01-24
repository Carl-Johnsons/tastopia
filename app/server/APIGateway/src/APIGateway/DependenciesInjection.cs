using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using APIGateway.Middleware;
using Ocelot.Middleware;
using Contract.Utilities;
using APIGateway.Extensions;
using Ocelot.Provider.Consul;
using APIGateway.Interfaces;
using Serilog;
using APIGateway.Services;
using Consul;

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

        builder.ConfigureSerilog();
        builder.ConfigureKestrel();


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

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var consulRegistryService = serviceProvider.GetRequiredService<IConsulRegistryService>();
                var identityUri = consulRegistryService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_IDENTITY", "Not found"));
                Log.Information("Connect to Identity Provider: " + identityUri!.ToString());

                options.Authority = identityUri!.ToString();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                    // Skip the validate issuer signing key
                    //ValidateIssuerSigningKey = false,
                    //SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    //{
                    //    var jwt = new JsonWebToken(token);

                    //    return jwt;
                    //},
                    //ValidIssuers = [
                    //    IdentityServerEndpoint
                    //],
                };
                // For development only
                options.IncludeErrorDetails = true;
            });
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

        app.UseMiddleware<HttpsFallbackMiddleware>();

        app.UseCors("AllowAnyOriginPolicy");

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseOcelot().Wait();
        return app;
    }
}

