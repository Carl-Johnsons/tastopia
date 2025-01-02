using Serilog;
using System.Text.Json.Serialization;
using UploadFileService.API.Middleware;
using UploadFileService.Application;
using UploadFileService.Infrastructure;

namespace UploadFileService.API;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        UploadFileService.Infrastructure.Utilities.EnvUtility.LoadEnvFile();
        var services = builder.Services;
        var config = builder.Configuration;
        var host = builder.Host;

        host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

        services.AddApplicationServices();
        services.AddInfrastructureServices(config);

        services.AddControllers()
                // Prevent circular JSON reach max depth of the object when serialization
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return builder;
    }

    public static WebApplication UseAPIServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseGlobalHandlingErrorMiddleware();

        return app;
    }
}

