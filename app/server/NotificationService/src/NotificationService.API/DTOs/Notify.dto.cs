namespace NotificationService.API.DTOs;

public class NotifyDTO
{
    public List<Guid> RecipientIds { get; set; } = [];
    public string Message { get; set; } = null!;
    public object? Data { get; set; } = null!;
    public string? Title { get; set; } = null!;
    public string? ChannelId { get; set; } = null!;
}
