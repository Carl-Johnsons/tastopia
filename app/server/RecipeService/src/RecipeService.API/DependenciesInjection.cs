using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Serilog;
using RecipeService.Application;
using RecipeService.Infrastructure;
using RecipeService.API.Middleware;
using Newtonsoft.Json;
using AutoMapper;
using RecipeService.API.Configs;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Contract.Utilities;

namespace RecipeService.API;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();
        var services = builder.Services;
        var config = builder.Configuration;
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

        host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

        services.AddApplicationServices();
        services.AddInfrastructureServices(config);

        // Register automapper
        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddControllers()
                // Prevent circular JSON reach max depth of the object when serialization
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                }).AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;
                });

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

        services.AddHttpContextAccessor();

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                var IdentityDNS = DotNetEnv.Env.GetString("IDENTITY_SERVER_HOST", "localhost:7001").Replace("\"", "");
                var IdentityServerEndpoint = $"https://{IdentityDNS}";
                Console.WriteLine("Connect to Identity Provider: " + IdentityServerEndpoint);

                options.Authority = IdentityServerEndpoint;
                // Clear default Microsoft's JWT claim mapping
                // Ref: https://stackoverflow.com/questions/70766577/asp-net-core-jwt-token-is-transformed-after-authentication
                options.MapInboundClaims = false;

                options.TokenValidationParameters.ValidTypes = ["at+jwt"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                // For development only
                options.IncludeErrorDetails = true;
            });

        services.AddEndpointsApiExplorer();
        return builder;
    }

    public static async Task<WebApplication> UseAPIServicesAsync(this WebApplication app)
    {

        app.Use(async (context, next) =>
        {
            // Log information about the incoming request
            app.Logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

            await next(); // Call the next middleware
        });

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.ConfigObject.PersistAuthorization = true;
            c.InjectJavascript("/Swagger/inject-access-token.js");
        });

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseGlobalHandlingErrorMiddleware();

        app.UseAuthentication();

        app.UseAuthorization();

        //try
        //{
        //    var signalRService = app.Services.GetService<ISignalRService>();
        //    await signalRService!.StartConnectionAsync();
        //}
        //catch (Exception ex)
        //{
        //    app.Logger.LogError($"Error connecting to SignalR: {ex.Message}");
        //}
        return app;
    }
}

