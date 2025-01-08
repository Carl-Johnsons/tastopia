using Microsoft.IdentityModel.Tokens;
using Serilog;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Ocelot.DependencyInjection;
using APIGateway.Middleware;
using Ocelot.Middleware;
using Contract.Utilities;

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

        var httpPort = DotNetEnv.Env.GetInt("PORT", 0);
        var httpsPort = DotNetEnv.Env.GetInt("HTTPS_PORT", 0);
        var certPath = DotNetEnv.Env.GetString("ASPNETCORE_Kestrel__Certificates__Default__Path");
        var certPassword = DotNetEnv.Env.GetString("ASPNETCORE_Kestrel__Certificates__Default__Password");

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(httpPort, listenOption =>
            {
                listenOption.Protocols = HttpProtocols.Http1;
            });

            options.ListenAnyIP(httpsPort, listenOption =>
            {
                listenOption.Protocols = HttpProtocols.Http1AndHttp2;
                // Can't use directly from dotnetenv, have to assign to an variable. Weird bug
                listenOption.UseHttps(certPath, certPassword);
            });
        });

        host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });


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
        services.AddEndpointsApiExplorer(); // This require for swaggerForOcelot to launch

        services.AddOcelot();

        services.AddSwaggerForOcelot(config);

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                var IdentityDNS = DotNetEnv.Env.GetString("IDENTITY_SERVER_HOST", "localhost:7001").Replace("\"", "");
                var IdentityServerEndpoint = $"https://{IdentityDNS}";
                Console.WriteLine("Connect to Identity Provider: " + IdentityServerEndpoint);

                options.Authority = IdentityServerEndpoint;
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

    public static async Task<WebApplication> UseAPIServicesAsync(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerForOcelotUI();

        app.UseMiddleware<HttpsFallbackMiddleware>();
        app.UseSerilogRequestLogging();

        app.UseCors("AllowAnyOriginPolicy");

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseOcelot().Wait();
        return app;
    }
}

