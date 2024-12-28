using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

[Table("UserSetting")]
[PrimaryKey(nameof(UserId), nameof(SettingId))]
public class UserSetting
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid SettingId { get; set; }
    public string SettingValue { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public virtual Setting Setting { get; set; } = null!;
}
