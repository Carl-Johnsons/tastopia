using System.ComponentModel.DataAnnotations;

namespace NotificationService.API.DTOs;

public class SaveExpoPushTokenDTO
{
    [Required]
    public string ExpoPushToken { get; set; } = null!;
}
