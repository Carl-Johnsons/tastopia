using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.OpenTelemetry;

namespace Contract.Extension;

public static class LoggingExtension
{
    public static IHostBuilder ConfigureLoggingService(this IHostBuilder builder, string serviceName)
    {
        var outputTemplate = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: outputTemplate)
            .CreateBootstrapLogger();

        builder.UseSerilog((ctx, lc) => lc
            .WriteTo.Console(outputTemplate: outputTemplate)
            .WriteTo.OpenTelemetry(options =>
            {
                options.Endpoint = Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT") ?? "http://localhost:4317";
                options.Protocol = OtlpProtocol.Grpc;
                options.ResourceAttributes = new Dictionary<string, object>
                {
                    ["service.name"] = serviceName
                };
            })
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(ctx.Configuration), preserveStaticLogger: true);
        return builder;
    }

    public static WebApplicationBuilder ConfigureLoggingService(this WebApplicationBuilder builder, string serviceName)
    {
        builder.Host.ConfigureLoggingService(serviceName);
        return builder;
    }

    public static WebApplication UseLoggingServices(this WebApplication app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
            options.GetLevel = (httpContext, elapsed, ex)
                    => IsExcludedPaths(httpContext) ? LogEventLevel.Debug : LogEventLevel.Information;
        });
        return app;
    }

    private static bool IsExcludedPaths(HttpContext ctx)
    {
        List<string> excludedPaths = ["/health"];
        var requestPath = ctx.Request.Path.Value;

        if (requestPath == null)
        {
            return false;
        }

        return excludedPaths.Any(path => requestPath.StartsWith(path, StringComparison.OrdinalIgnoreCase));
    }
}
