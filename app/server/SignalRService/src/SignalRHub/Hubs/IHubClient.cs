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
    Task OnlineUserNumberChanged(int number);
    Task TotalRecipeNumberChanged(int number);
    Task TotalUserNumberChanged(int number);


}
