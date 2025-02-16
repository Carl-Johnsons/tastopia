using MassTransit;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.NotificationEvent;

[EntityName("PushNotificationEvent")]
public class PushNotificationEvent
{
    [Required]
    public List<string> ExpoPushTokens { get; set; } = [];
    [Required]
    public string Message { get; set; } = null!;
    [JsonProperty("data")]
    public string? JsonData { get; set; } = null!;
    public string? ChannelId { get; set; } = null!;
    public string? Title { get; set; } = null!;
}
