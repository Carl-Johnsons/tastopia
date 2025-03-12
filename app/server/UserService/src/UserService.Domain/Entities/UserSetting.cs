using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

[Table("UserSetting")]
[PrimaryKey(nameof(AccountId), nameof(SettingId))]
public class UserSetting
{
    [Required]
    public Guid AccountId { get; set; }
    [Required]
    public Guid SettingId { get; set; }
    public string SettingValue { get; set; } = null!;
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
    public virtual Setting Setting { get; set; } = null!;
}
