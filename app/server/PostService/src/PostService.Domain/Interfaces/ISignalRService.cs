using Microsoft.AspNetCore.SignalR.Client;

namespace PostService.Domain.Interfaces;

public interface ISignalRService
{
    HubConnection HubConnection { get; init; }

    Task StartConnectionAsync();
    Task StopConnectionAsync();
    Task InvokeAction<T>(string action, T obj);
    Task InvokeAction(string action);
}