namespace Contract.DTOs.SignalRDTO;

public class InvalidateNotificationDTO
{
    public List<Guid> RecipientIds { get; set; } = [];
}
