using SignalRHub.DTOs;

namespace SignalRHub.Hubs;

public interface IHubClient
{
    Task Connected(IEnumerable<Guid>? userIdOnlineList);
    Task Disconnected(Guid userDisconnectedId);
    Task ReceiveTest();
    Task ReceiveTest(string response);
    Task ReceiveTest(TestObject obj);
    Task ReceiveNotification();
    Task ForcedLogout();
    Task ReceiveOnlineUserNumber(int number);
    Task ReceiveTotalRecipeNumber(int number);
    Task ReceiveTotalUserNumber(int number);


}
