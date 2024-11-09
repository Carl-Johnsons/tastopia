using AutoMapper;
using Contract.Event.FriendEvent;
using Contract.Event.UploadEvent;
using Contract.Event.UserEvent;
using Duende.IdentityServer;
using DuendeIdentityServer.Data;
using DuendeIdentityServer.DTOs;
using DuendeIdentityServer.Models;
using DuendeIdentityServer.Services;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace DuendeIdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        DotNetEnv.Env.Load(".env");
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
        {
            DotNetEnv.Env.Load(".env.production");
        }
        var reactUrl = Environment.GetEnvironmentVariable("REACT_URL") ?? "http://localhost:3000";
        var issuer = Environment.GetEnvironmentVariable("ISSUER") ?? "http://localhost:5001";


        var services = builder.Services;

        services.AddRazorPages()
                .AddRazorRuntimeCompilation();

        services.AddControllers();

        services.AddDbContext<ApplicationDbContext>();


        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton<ISignalRService, SignalRService>();
        services.AddScoped(typeof(IPaginateDataUtility<,>), typeof(PaginateDataUtility<,>));

        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();

            //busConfig.UsingInMemory((context, config) => config.ConfigureEndpoints(context));
            busConfig.UsingRabbitMq((context, config) =>
            {
                var username = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER") ?? "admin";
                var password = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS") ?? "pass";
                var rabbitMQHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost:5672";

                config.Host($"amqp://{rabbitMQHost}/", host =>
                {
                    host.Username(username);
                    host.Password(password);
                });
                config.ConfigureEndpoints(context);

                config.Message<FriendCreatedEvent>(m =>
                {
                    m.SetEntityName("friend-created-event"); // Explicit exchange name
                });

                config.Message<UserBlockedEvent>(u =>
                {

                });

            });

            busConfig.AddRequestClient<UploadMultipleFileEvent>(new Uri("queue:upload-multiple-file-event-queue"));
            busConfig.AddRequestClient<UploadMultipleFileEvent>(new Uri("queue:delete-multiple-file-event-queue"));
            busConfig.AddRequestClient<UploadMultipleFileEvent>(new Uri("queue:update-file-event-queue"));

        });


        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
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
                options.Discovery.CustomEntries.Add("user-api", "~/api/users");
                options.Discovery.CustomEntries.Add("friend-request-api", "~/api/users/friend-request");

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
            .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<ProfileService>();

        //   .AddDeveloperSigningCredential(); // not recommended for production


        services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5001/signin-google
                options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") ?? "";
                options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") ?? "";

                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
            });

        services.AddLocalApiAuthentication();


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

        // Chrome using SameSite.None with https scheme. But host is4 with http scheme so SameSiteMode.Lax is required
        app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

        app.UseCors("AllowSpecificOrigins");

        app.UseStaticFiles();
        app.UseRouting();
        // UseIdentityServer already call UseAuthenticate()
        app.UseIdentityServer();
        app.UseAuthorization();

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