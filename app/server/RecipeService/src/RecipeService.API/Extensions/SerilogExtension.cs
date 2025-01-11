using Serilog;

namespace RecipeService.API.Extensions;

public static class SerilogExtension
{
    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(ctx.Configuration), preserveStaticLogger: true);
        return builder;
    }

    public static WebApplication UseSerilogServices(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }
}