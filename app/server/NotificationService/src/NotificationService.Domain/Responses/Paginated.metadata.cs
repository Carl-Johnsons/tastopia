using Newtonsoft.Json;

namespace NotificationService.Domain.Responses;

public class NotificationListMetadata : AdvancePaginatedMetadata
{
    [JsonProperty("unreadNotifications")]
    public int UnreadNotifications { get; set; }
}
