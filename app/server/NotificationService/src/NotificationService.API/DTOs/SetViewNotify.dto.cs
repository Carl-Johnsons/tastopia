using System.ComponentModel.DataAnnotations;

namespace NotificationService.API.DTOs;

public class SetViewNotifyDTO
{
    [Required]
    [JsonProperty("notificationId")]
    public Guid NotificationId { get; set; }
}
