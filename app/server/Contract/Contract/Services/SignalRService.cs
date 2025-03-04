using Contract.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Contract.Services;

public sealed class SignalRService : ISignalRService
{
    private readonly IConsulRegistryService _consulRegistryService;
    private readonly ILogger<SignalRService> _logger;
    public HubConnection HubConnection { get; init; }

    public SignalRService(ILogger<SignalRService> logger, IConsulRegistryService consulRegistryService)
    {
        _logger = logger;
        _consulRegistryService = consulRegistryService;
        var webSocketUri = _consulRegistryService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_SIGNALR"));
        _logger.LogInformation("Connect to chat hub " + webSocketUri);

        HubConnection = new HubConnectionBuilder()
            .WithUrl($"{webSocketUri}tastopia-hub")
            .WithAutomaticReconnect(
            [
                TimeSpan.Zero,
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(30)
            ])
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
        await HubConnection.InvokeAsync(action);
    }
}
