using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.NotificationEvent;

[EntityName("PushNotificationEvent")]
public class PushNotificationEvent
{
    [Required]
    public List<string> ExpoPushToken { get; set; } = [];
    [Required]
    public string Message { get; set; } = null!;
}
