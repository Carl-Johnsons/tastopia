using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Entities;

public class UserSetting : BaseAuditableEntity
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid SettingId { get; set; }
    public string SettingValue { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public virtual Setting Setting { get; set; } = null!;
}
