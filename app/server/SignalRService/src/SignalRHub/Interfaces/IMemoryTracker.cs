namespace SignalRHub.Interfaces;

public interface IMemoryTracker
{
    public int OnlineUserNumber { get; set; }
    public int TotalRecipe { get; set; }
    public int TotalUser { get; set; }
    public void UserConnected(string userId);
    public void UserDisconnected(string userId);
}
