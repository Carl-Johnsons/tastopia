namespace NotificationService.API.DTOs;

public class NotifyDTO
{
    public List<Guid> RecipientIds { get; set; } = [];
    public string Message { get; set; } = null!;
}
