using Contract.Extension;
using Contract.Utilities;
using Duende.IdentityServer;
using Duende.IdentityServer.ResponseHandling;
using DuendeIdentityServer.Extensions;
using DuendeIdentityServer.Services;
using IdentityService.Application;
using IdentityService.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Net;

namespace DuendeIdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();

        var websiteUrl = DotNetEnv.Env.GetString("WEBSITE_CLIENT_URL", "http://localhost:3000");
        var issuer = DotNetEnv.Env.GetString("ISSUER", "http://localhost:5001");
        var services = builder.Services;

        builder.ConfigureLoggingService()
               .ConfigureKestrel()
               .ConfigureHealthCheck();

        services.AddInfrastructureServices()
                .AddApplicationServices()
                .AddGrpcServices()
                .AddSwaggerServices();

        services.AddRazorPages()
                .AddRazorRuntimeCompilation();

        services.AddCommonAPIServices();

        // Register automapper
        services.AddAutoMapper(
            cfg =>
            {
                cfg.LicenseKey = DotNetEnv.Env.GetString("LUCKYPENNYSOFTWARE_LICENSE_KEY", "Not Found");
            },
            AppDomain.CurrentDomain.GetAssemblies());

        services
            .AddIdentityServer(options =>
            {
                options.IssuerUri = issuer;

                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;

                // Automatic key management
                options.KeyManagement.RotationInterval = TimeSpan.FromDays(30);
                //   announce new key 2 days in advance in discovery
                options.KeyManagement.PropagationTime = TimeSpan.FromDays(2);
                //   keep old key for 7 days in discovery for validation of tokens
                options.KeyManagement.RetentionDuration = TimeSpan.FromDays(7);
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseNpgsql(EnvUtility.GetConnectionString(),
                                        options => options.MigrationsAssembly("IdentityService.Infrastructure")
                                                            .EnableRetryOnFailure(
                                                                maxRetryCount: 10,
                                                                maxRetryDelay: TimeSpan.FromSeconds(15),
                                                                errorCodesToAdd: null
                                                            ));

                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 3600;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<ApplicationAccount>()
            .AddProfileService<ProfileService>()
            .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();

        //  .AddDeveloperSigningCredential(); // not recommended for production

        services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to https://localhost:5001/signin-google
                    options.ClientId = DotNetEnv.Env.GetString("GOOGLE_CLIENT_ID", "");
                    options.ClientSecret = DotNetEnv.Env.GetString("GOOGLE_CLIENT_SECRET", "");

                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");

                    // Map google picture's claim to simple claim for easier query
                    options.ClaimActions.MapJsonKey("picture", "picture");

                    options.SaveTokens = true;

                    // Config cookie
                    options.CorrelationCookie.SameSite = SameSiteMode.Lax;
                    options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.None;
                });

        services.AddLocalApiAuthentication();

        services.AddScoped<IAuthorizeInteractionResponseGenerator, CustomAuthorizeInteractionResponseGenerator>();

        // Config data protection persisted to Redis for multi-instance support
        var redis = ConnectionMultiplexer.Connect(
            new ConfigurationOptions
            {
                EndPoints =
                {
                    $"{DotNetEnv.Env.GetString("REDIS_HOST", "Not Found")}:{DotNetEnv.Env.GetString("REDIS_PORT", "Not Found")}"
                },
                Password = DotNetEnv.Env.GetString("REDIS_PASSWORD", ""),
                AbortOnConnectFail = false
            }
        );
        redis.ConnectionFailed += (_, e) =>
        {
            Serilog.Log.Error(
                $"Redis connection failed: {e.EndPoint}, {e.FailureType}, {e.Exception?.Message}"
            );
        };

        redis.ConnectionRestored += (_, e) =>
        {
            Serilog.Log.Information(
                $"Redis connection restored: {e.EndPoint}"
            );
        };

        if (redis.IsConnected)
        {
            try
            {
                var latency = redis.GetDatabase().Ping();

                Serilog.Log.Information(
                    "Redis connected successfully. Endpoint: {Endpoints}, Ping: {Latency}",
                    string.Join(", ", redis.GetEndPoints().Select(x => x.ToString())),
                    latency
                );
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Redis connection test failed.");
            }
        }
        else
        {
            Serilog.Log.Warning(
                "Redis multiplexer created, but Redis is not currently connected."
            );
        }
        services.AddDataProtection()
            .PersistKeysToStackExchangeRedis(redis, "tastopia-identity-dataprotection-keys")
            .SetApplicationName("tastopia-identity");

        // CORS policy config
        services.AddCors(o => o.AddPolicy("AllowSpecificOrigins", builder =>
        {
            builder.WithOrigins(websiteUrl, "http://api-gateway", "http://localhost:5000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }));


        return builder.Build();
    }

    public static Task<WebApplication> ConfigurePipeline(this WebApplication app)
    {
        var forwardedHeadersOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost
        };
        forwardedHeadersOptions.KnownNetworks.Clear();
        forwardedHeadersOptions.KnownProxies.Clear();
        app.UseForwardedHeaders(forwardedHeadersOptions);

        app.Use(async (context, next) =>
        {
            // Add your custom CSP header, allowing images from res.cloudinary.com.
            context.Response.Headers.Append("Content-Security-Policy",
                                "default-src 'self'; img-src 'self' https://res.cloudinary.com;");

            await next();
        });

        app.UseInfrastructureServices()
           .UseSwaggerServices()
           .UseCommonAPIMiddleware();

        // Chrome using SameSite.None with https scheme. But host is4 with http scheme so SameSiteMode.Lax is required
        app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

        //app.UseCookiePolicy();

        //app.UseCookiePolicy(new CookiePolicyOptions
        //{
        //    MinimumSameSitePolicy = SameSiteMode.None,
        //    Secure = EnvUtility.IsDevelopment()
        //        ? CookieSecurePolicy.SameAsRequest // Allow http in development
        //        : CookieSecurePolicy.Always        // Enforce https in production
        //});

        app.UseCors("AllowSpecificOrigins");
        app.UseStaticFiles();

        // UseIdentityServer already call UseAuthenticate()
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.UseGrpcServices()
           .UseCustomHealthCheck();

        app.MapRazorPages();

        // Add a user api endpoint so this will not be a minimal API
#pragma warning disable ASP0014
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute()
                .RequireAuthorization();
        });

        app.UseSignalRServiceAsync();
        // app.Use(async (context, next) =>
        //     {
        //         Console.WriteLine($"RemoteIp: {context.Connection.RemoteIpAddress}");
        //         await next();
        //     });

        return Task.FromResult(app);
    }
}
