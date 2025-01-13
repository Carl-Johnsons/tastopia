using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using APIGateway;

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
        Console.WriteLine($"API gateway is listening on: {address}");
    }
}
else
{
    Console.WriteLine("Could not retrieve server addresses.");
}

app.WaitForShutdown();