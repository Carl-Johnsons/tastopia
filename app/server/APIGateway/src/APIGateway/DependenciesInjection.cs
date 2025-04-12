using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Contract.Utilities;
using APIGateway.Extensions;
using Ocelot.Provider.Consul;
using Contract.Extension;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Serilog;

namespace APIGateway;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();
        var websiteUrl = DotNetEnv.Env.GetString("WEBSITE_CLIENT_URL", "http://localhost:3000");
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
                .AddDelegatingHandler<SecretHeaderHandler>(global: true)
                .AddConsul();

        services.AddSwaggerServices(config);

        services.AddAPIGatewayAPIServices();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOriginPolicy",
                builder =>
                {
                    builder.WithOrigins(websiteUrl)
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
        app.UseConsulServiceDiscovery(DotNetEnv.Env.GetString("CONSUL_API_GATEWAY", "Not Found"));

        app.UseRouting();
        app.UseCustomHealthCheck();
        app.UseCors("AllowAnyOriginPolicy");

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseWebSockets();

#pragma warning disable ASP0014 // Suggest using top level route registrations
        app.UseEndpoints(e =>
        {
            e.MapControllers();
        });
#pragma warning restore ASP0014 // Suggest using top level route registrations

        app.UseOcelot(new OcelotPipelineConfiguration
        {
            AuthorizationMiddleware = async (ctx, next) =>
            {
                if (Authorize(ctx))
                {
                    var userRoleClaims = ctx.User.Claims
                        .Where(c => c.Type.Equals("role", StringComparison.OrdinalIgnoreCase))
                        .Select(c => c.Value);
                    var routeClaims = ctx.Items.DownstreamRoute().RouteClaimsRequirement;
                    string requiredRole = routeClaims.TryGetValue("role", out var val) ? val : "Not specified";
                    Log.Information(
                        "Authorization successfully. User roles: {UserRoles}. Required role: {RequiredRole}",
                        string.Join(", ", userRoleClaims),
                        requiredRole
                    );

                    await next.Invoke();
                }
                else
                {
                    var userRoleClaims = ctx.User.Claims
                        .Where(c => c.Type.Equals("role", StringComparison.OrdinalIgnoreCase))
                        .Select(c => c.Value);
                    var routeClaims = ctx.Items.DownstreamRoute().RouteClaimsRequirement;
                    string requiredRole = routeClaims.TryGetValue("role", out var val) ? val : "Not specified";
                    Log.Warning(
                        "Authorization failed. User roles: {UserRoles}. Required role: {RequiredRole}",
                        string.Join(", ", userRoleClaims),
                        requiredRole
                    );
                    ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                }
            }
        }).Wait();

        return app;
    }

    private static bool Authorize(HttpContext ctx)
    {
        if (ctx.Items.DownstreamRoute().AuthenticationOptions.AuthenticationProviderKey == null) return true;
        else
        {

            bool auth = false;
            Claim[] claims = ctx.User.Claims.ToArray<Claim>();
            Dictionary<string, string> required = ctx.Items.DownstreamRoute().RouteClaimsRequirement;

            // If there's no role requirement specified, return true.
            if (!required.ContainsKey("role") || string.IsNullOrWhiteSpace(required["role"]))
            {
                return true;
            }

            Regex reor = new Regex(@"[^,\s+$ ][^\,]*[^,\s+$ ]");
            MatchCollection matches;

            Regex reand = new Regex(@"[^&\s+$ ][^\&]*[^&\s+$ ]");
            MatchCollection matchesand;
            int cont = 0;
            foreach (KeyValuePair<string, string> claim in required)
            {
                matches = reor.Matches(claim.Value);
                foreach (Match match in matches)
                {
                    matchesand = reand.Matches(match.Value);
                    cont = 0;
                    foreach (Match m in matchesand)
                    {
                        foreach (Claim cl in claims)
                        {
                            if (cl.Type == claim.Key)
                            {
                                if (cl.Value == m.Value)
                                {
                                    cont++;
                                }
                            }
                        }
                    }
                    if (cont == matchesand.Count)
                    {
                        auth = true;
                        break;
                    }
                }
            }
            return auth;
        }
    }

}

