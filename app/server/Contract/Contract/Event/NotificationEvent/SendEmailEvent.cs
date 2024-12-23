using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.NotificationEvent;

[EntityName("email-worker-exchange")]
public class SendEmailEvent
{
    [Required]
    public string EmailTo { get; set; } = null!;
    [Required]
    public string Subject { get; set; } = null!;
    [Required]
    public string Body { get; set; } = null!;
    public bool IsHTML { get; set; } = false;
}
