using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;
using SignalRHub.DTOs;
using Contract.Interfaces;
using Contract.DTOs.SignalRDTO;
using SignalRHub.Interfaces;

namespace SignalRHub.Hubs;

public class HubServer : Hub<IHubClient>
{
    //Use the dictionary to map the userId and userConnectionId
    private static readonly ConcurrentDictionary<string, Guid> UserConnectionMap = new();
    private readonly IHttpContextAccessor _httpContextAccessor;
    // rabbitmq
    private readonly IServiceBus _bus;
    private readonly IMemoryTracker _memoryTracker;
    public HubServer(IHttpContextAccessor httpContextAccessor,
                     IServiceBus bus,
                     IMemoryTracker memoryTracker)
    {
        _httpContextAccessor = httpContextAccessor;
        _bus = bus;
        _memoryTracker = memoryTracker;
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
                _memoryTracker.UserConnected(userId);
                await Clients.Group("Admin").OnlineUserNumberChanged(_memoryTracker.OnlineUserNumber);
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
            _memoryTracker.UserDisconnected(userDisconnectedId.ToString());
            await Clients.Group("Admin").OnlineUserNumberChanged(_memoryTracker.OnlineUserNumber);
        }
        else
        {
            Log.Error($"Connection {Context.ConnectionId} disconnected, but it was not found in UserConnectionMap.");
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task CreateRecipe(int totalRecipe)
    {
        await Clients.Group("Admin").TotalRecipeNumberChanged(totalRecipe);
    }

    public async Task UserRegister(int totalUser)
    {
        await Clients.Group("Admin").TotalUserNumberChanged(totalUser);
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

    public async Task InvalidateNotification(InvalidateNotificationDTO dto)
    {
        // Have to get list because the 1 person can join on 2 different tab on browser
        // So the connectionId may different but still 1 userId
        var receiverConnectionIdList = UserConnectionMap.
            Where(pair => dto.RecipientIds.Contains(pair.Value))
            .Select(pair => pair.Key)
            .ToList();

        // If the receiver didn't online, simply do nothing
        if (receiverConnectionIdList.Count <= 0)
        {
            return;
        }
        var tasks = receiverConnectionIdList
            .Select(receiverConnectionId => Clients.Client(receiverConnectionId).ReceiveNotification())
            .ToArray();

        await Task.WhenAll(tasks);
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
