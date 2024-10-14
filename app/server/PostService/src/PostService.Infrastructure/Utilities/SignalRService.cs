using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace PostService.Infrastructure.Utilities;

public sealed class SignalRService : ISignalRService
{
    private readonly ILogger<SignalRService> _logger;
    public HubConnection HubConnection { get; init; }

    public SignalRService(ILogger<SignalRService> logger)
    {
        _logger = logger;
        var websocketHost = (Environment.GetEnvironmentVariable("WEBSOCKET_HOST") ?? "localhost:5003").Replace("\"", "");
        Console.WriteLine("Connect to chat hub " + websocketHost);
        HubConnection = new HubConnectionBuilder()
            .WithUrl($"http://{websocketHost}/chat-hub")
            .WithAutomaticReconnect([TimeSpan.Zero, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(30)])
            .ConfigureLogging(logging =>
            {
                logging.SetMinimumLevel(LogLevel.Information);
            })
            .Build();
    }

    public async Task StartConnectionAsync()
    {
        _logger.LogInformation("Connecting to signalR");
        await HubConnection.StartAsync();
        _logger.LogInformation("SignalR connected");
    }

    public async Task StopConnectionAsync()
    {
        await HubConnection.StopAsync();
        _logger.LogInformation("SignalR disconnected");
    }
    public async Task InvokeAction<T>(string action, T obj)
    {
        _logger.LogInformation($"Begin to invoke {action} with DTO: ${JsonConvert.SerializeObject(obj)}");
        await HubConnection.InvokeAsync(action, obj);
        _logger.LogInformation($"Done invoking action");
    }
    public async Task InvokeAction(string action)
    {
        _logger.LogInformation($"Begin to invoke {action}");
        await HubConnection.InvokeAsync(action);
        _logger.LogInformation($"Done invoking action");
    }
}
