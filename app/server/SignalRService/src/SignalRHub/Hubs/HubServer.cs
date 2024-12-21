using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http.Extensions;
using MassTransit;
using Serilog;
using SignalRHub.DTOs;

namespace SignalRHub.Hubs;

public class HubServer : Hub<IHubClient>
{
    //Use the dictionary to map the userId and userConnectionId
    private static readonly ConcurrentDictionary<string, Guid> UserConnectionMap = new();
    private readonly IHttpContextAccessor _httpContextAccessor;
    // rabbitmq
    private readonly IBus _bus;
    public HubServer(IHttpContextAccessor httpContextAccessor, IBus bus)
    {
        _httpContextAccessor = httpContextAccessor;
        _bus = bus;
    }

    // The url would be like "https://yourhubURL:port?userId=abc&access_token=abc"
    public override async Task OnConnectedAsync()
    {
        Log.Information("Connected");
        var userId = _httpContextAccessor.HttpContext?.Request.Query["userId"].ToString();
        try
        {

            if (string.IsNullOrEmpty(userId))
            {
                var RequestUrl = Context.GetHttpContext()?.Request.GetDisplayUrl() ?? "Unknown";
                Log.Information($"Service with url {RequestUrl} has connected to signalR sucessfully!");
            }
            else
            {
                Log.Information($"user with id {userId} has connected to signalR sucessfully!");
                await ConnectWithUserIdAsync(Guid.Parse(userId));
            }

        }
        catch (Exception ex)
        {
            Log.Error($"Error: {ex.Message}");
        }

        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Log.Information($"Disconnecting from signalR!");
        if (UserConnectionMap.TryRemove(Context.ConnectionId, out Guid userDisconnectedId))
        {
            Log.Information($"Connection {Context.ConnectionId} disconnected and removed from UserConnectionMap.");
            await Clients.All.Disconnected(userDisconnectedId);
        }
        else
        {
            Log.Error($"Connection {Context.ConnectionId} disconnected, but it was not found in UserConnectionMap.");
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task LeaveGroup(int groupId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId.ToString());
    }

    public async Task TestEvent()
    {
        await Clients.All.ReceiveTest();
    }

    public async Task TestEventWithParams(string text)
    {
        await Clients.All.ReceiveTest(text);
    }

    public async Task TestEventWithObjectParams(TestObject obj)
    {
        await Clients.All.ReceiveTest(obj);
    }


    #region Helper method
    private async Task ConnectWithUserIdAsync(Guid userId)
    {
        UserConnectionMap[Context.ConnectionId] = userId;
        Log.Information($"Map user complete with {Context.ConnectionId} and {userId}");
        Log.Information(userId + " Connected");

        // Admin user
        if (Context.User != null && Context.User.IsInRole("Admin"))
        {
            Log.Information("Add admin to group admin");
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
            return;
        }

        foreach (var key in UserConnectionMap.Keys)
        {
            Log.Information($"{key}: {UserConnectionMap[key]}");
        }
        var userIdOnlineList = UserConnectionMap.Select(uc => uc.Value);
        await Clients.All.Connected(userIdOnlineList);
    }
    #endregion
}
