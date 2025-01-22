using MongoDB.EntityFrameworkCore;
using NotificationService.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Domain.Entities;

[Collection("AccountExpoPushToken")]
public class AccountExpoPushToken : BaseMongoDBAuditableEntity
{
    [Required]
    public Guid AccountId { get; set; }
    [Required]
    public string ExpoPushToken { get; set; } = null!;
    [Required]
    public DeviceType DeviceType { get; set; }
}
