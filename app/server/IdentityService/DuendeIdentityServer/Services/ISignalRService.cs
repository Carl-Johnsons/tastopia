using Microsoft.AspNetCore.SignalR.Client;

namespace DuendeIdentityServer.Services
{
    public interface ISignalRService
    {
        HubConnection HubConnection { get; init; }

        Task InvokeAction<T>(string action, T obj);
        Task StartConnectionAsync();
        Task StopConnectionAsync();
    }
}