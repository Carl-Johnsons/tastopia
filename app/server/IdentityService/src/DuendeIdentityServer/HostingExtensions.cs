using Contract.Utilities;
using Duende.IdentityServer;
using Duende.IdentityServer.ResponseHandling;
using DuendeIdentityServer.Middleware;
using DuendeIdentityServer.Services;
using IdentityService.Application;
using IdentityService.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using Serilog;

namespace DuendeIdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();

        var reactUrl = DotNetEnv.Env.GetString("REACT_URL", "http://localhost:3000");
        var issuer = DotNetEnv.Env.GetString("ISSUER", "http://localhost:5001");

        var services = builder.Services;
        var host = builder.Host;

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

        services.AddApplicationServices();
        services.AddInfrastructureServices(builder.Configuration);
        services.AddGrpc();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Input your Bearer token in the following format: `Bearer {your_token}`"
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

        services.AddRazorPages()
                .AddRazorRuntimeCompilation();

        services.AddControllers();

        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton<ISignalRService, SignalRService>();
        services.AddScoped(typeof(IPaginateDataUtility<,>), typeof(PaginateDataUtility<,>));

        services.AddScoped<IServiceBus, MassTransitServiceBus>();

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

                // Config cookie
                //options.CorrelationCookie.SameSite = SameSiteMode.None;
                //options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
            });

        services.AddLocalApiAuthentication();

        services.AddScoped<IAuthorizeInteractionResponseGenerator, CustomAuthorizeInteractionResponseGenerator>();

        // Config data protection
        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory() + "/keys"))
            .SetApplicationName("tastopia");

        services.AddCors(o => o.AddPolicy("AllowSpecificOrigins", builder =>
        {
            builder.WithOrigins(reactUrl, "http://api-gateway", "http://localhost:5000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }));


        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.ConfigObject.PersistAuthorization = true;
        });

        // Chrome using SameSite.None with https scheme. But host is4 with http scheme so SameSiteMode.Lax is required
        app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

        //app.UseCookiePolicy(new CookiePolicyOptions
        //{
        //    MinimumSameSitePolicy = SameSiteMode.None,
        //    Secure = app.Environment.IsDevelopment()
        //        ? CookieSecurePolicy.SameAsRequest // Allow http in development
        //        : CookieSecurePolicy.Always        // Enforce https in production
        //});

        app.UseCors("AllowSpecificOrigins");

        app.UseStaticFiles();
        app.UseRouting();
        // UseIdentityServer already call UseAuthenticate()
        app.UseIdentityServer();
        app.UseAuthorization();
        app.UseGlobalHandlingErrorMiddleware();
        app.MapRazorPages();

        // Add a user api endpoint so this will not be a minimal API
#pragma warning disable ASP0014
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute()
                .RequireAuthorization();
        });

        var signalRService = app.Services.GetService<ISignalRService>();
        signalRService!.StartConnectionAsync();
        return app;
    }
}
