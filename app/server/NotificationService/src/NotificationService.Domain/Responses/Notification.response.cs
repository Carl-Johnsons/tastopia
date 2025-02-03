namespace NotificationService.Domain.Responses;

public class NotificationsResponse
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string Message { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? JsonData { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
