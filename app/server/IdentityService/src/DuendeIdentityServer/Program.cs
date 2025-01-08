using DuendeIdentityServer;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Serilog;
using Contract.Utilities;

EnvUtility.LoadEnvFile();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    app.Start();

    var server = app.Services.GetService<IServer>();
    var addresses = server?.Features.Get<IServerAddressesFeature>()?.Addresses;

    if (addresses != null)
    {
        foreach (var address in addresses)
        {
            Console.WriteLine($"API is listening on: {address}");
        }
    }
    else
    {
        Console.WriteLine("Could not retrieve server addresses.");
    }

    app.WaitForShutdown();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
