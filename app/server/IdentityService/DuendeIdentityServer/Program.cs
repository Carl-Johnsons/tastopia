using DuendeIdentityServer;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Serilog;
using Serilog.Core;
using Serilog.Events;

DotNetEnv.Env.Load();
var levelSwitch = new LoggingLevelSwitch();
//var minimumLevel = Environment.GetEnvironmentVariable("Serilog__MinimumLevel") ?? "Information";
var minimumLevel = "Information";

// Set the initial log level based on the environment variable, defaulting to Information if parsing fails
levelSwitch.MinimumLevel = Enum.TryParse<LogEventLevel>(minimumLevel, true, out var parsedLevel)
    ? parsedLevel
    : LogEventLevel.Information;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .MinimumLevel.ControlledBy(levelSwitch)
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    // this seeding is only for the template to bootstrap the DB and users.
    // in production you will likely want a different approach.
    Log.Information("Seeding database...");
    var connectionString = Config.GetConnectionString();
    SeedData.EnsureSeedData(connectionString);
    Log.Information("Done seeding database");

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
