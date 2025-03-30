using SignalRHub.Interfaces;

namespace SignalRHub.Services;

public class MemoryTracker : IMemoryTracker
{
    public int OnlineUserNumber { get; set; } = 0;

    public int TotalRecipe { get; set; } = 0;

    public int TotalUser { get; set; } = 0;

    private HashSet<string> OnlineUserIds { get; set; } = new HashSet<string>();

    public void UserConnected(string userId)
    {
        OnlineUserIds.Add(userId);
        OnlineUserNumber = OnlineUserIds.Count;
    }

    public void UserDisconnected(string userId)
    {
        OnlineUserIds.Remove(userId);
        OnlineUserNumber = OnlineUserIds.Count;
    }
}
