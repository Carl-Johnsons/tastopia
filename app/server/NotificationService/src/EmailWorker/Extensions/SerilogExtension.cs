using Serilog;

namespace EmailWorker.Extensions;

public static class SerilogExtension
{
    public static IHostBuilder ConfigureSerilog(this IHostBuilder builder)
    {
        var outputTemplate = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: outputTemplate)
            .CreateBootstrapLogger();

        builder.UseSerilog((ctx, lc) => lc
            .WriteTo.Console(outputTemplate: outputTemplate)
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(ctx.Configuration), preserveStaticLogger: true);
        return builder;
    }
}