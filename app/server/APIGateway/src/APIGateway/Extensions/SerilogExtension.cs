using Serilog;
using Serilog.Events;

namespace APIGateway.Extensions;

public static class SerilogExtension
{
    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        var outputTemplate = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: outputTemplate)
            .CreateBootstrapLogger();

        builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console(outputTemplate: outputTemplate)
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(ctx.Configuration), preserveStaticLogger: true);
        return builder;
    }

    public static WebApplication UseSerilogServices(this WebApplication app)
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