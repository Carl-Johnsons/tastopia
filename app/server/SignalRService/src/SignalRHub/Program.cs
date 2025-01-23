using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using SignalRHub;

var builder = WebApplication.CreateBuilder(args)
                .AddChatHubServices();
var app = builder.Build();
app.UseChatHubService();

app.Start();

var server = app.Services.GetService<IServer>();
var addresses = server?.Features.Get<IServerAddressesFeature>()?.Addresses;

if (addresses != null)
{
    foreach (var address in addresses)
    {
    }
}
else
{
}

app.WaitForShutdown();
