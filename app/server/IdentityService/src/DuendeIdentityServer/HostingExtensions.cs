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
using Newtonsoft.Json;

namespace DuendeIdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();

        var websiteUrl = DotNetEnv.Env.GetString("WEBSITE_CLIENT_URL", "http://localhost:3000");
        var issuer = DotNetEnv.Env.GetString("ISSUER", "http://localhost:5001");
        var services = builder.Services;

        builder.ConfigureCommonAPIServices();

        services.AddInfrastructureServices();
        services.AddApplicationServices();
        services.AddErrorValidation();
        services.AddGrpcServices();
        services.AddSwaggerServices();

        services.AddRazorPages()
                .AddRazorRuntimeCompilation();


        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;
        });

        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddCommonAPIWithoutAuthServices();
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
            .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
            .AddCustomAuthorizeRequestValidator<CustomAuthorizeRequestValidator>();

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

        // Config data protection
        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory() + "/keys"))
            .SetApplicationName("tastopia");

        services.AddCors(o => o.AddPolicy("AllowSpecificOrigins", builder =>
        {
            builder.WithOrigins(websiteUrl, "http://api-gateway", "http://localhost:5000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }));


        return builder.Build();
    }

    public static async Task<WebApplication> ConfigurePipelineAsync(this WebApplication app)
    {
        if (EnvUtility.IsProduction())
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }

        app.Use(async (context, next) =>
        {
            // Add your custom CSP header, allowing images from res.cloudinary.com.
            context.Response.Headers.Append("Content-Security-Policy",
                                "default-src 'self'; img-src 'self' https://res.cloudinary.com;");

            await next();
        });


        app.UseCommonServices(DotNetEnv.Env.GetString("CONSUL_IDENTITY", "Not Found"));
        app.UseSwaggerServices();

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
        app.UseGrpcServices();
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

        app.UseSignalRServiceAsync();

        return app;
    }
}
