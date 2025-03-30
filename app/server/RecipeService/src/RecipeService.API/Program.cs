using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using RecipeService.API;
using Serilog;

try
{
    var app = await WebApplication.CreateBuilder(args)
                .AddAPIServices()
                .Build()
                .UseAPIServicesAsync();

    app.Start();

    var server = app.Services.GetService<IServer>();
    var addresses = server?.Features.Get<IServerAddressesFeature>()?.Addresses;

    if (addresses != null)
    {
        foreach (var address in addresses)
        {
            Log.Information($"API is listening on: {address}");
        }
    }
    else
    {
        Log.Information("Could not retrieve server addresses.");
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
