using SignalRHub.DTOs;

namespace SignalRHub.Hubs;

public interface IHubClient
{
    Task Connected(IEnumerable<Guid>? userIdOnlineList);
    Task Disconnected(Guid userDisconnectedId);
    Task ReceiveMessage(MessageDTO messageDTO);
    Task ForcedLogout();
}
