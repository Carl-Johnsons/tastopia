using MassTransit;
using System.ComponentModel.DataAnnotations;
namespace Contract.Event.NotificationEvent;
[EntityName("SendSMSEvent")]
public class SendSMSEvent
{
    [Required]
    public string PhoneTo { get; set; } = null!;
    [Required]
    public string Message { get; set; } = null!;
}
