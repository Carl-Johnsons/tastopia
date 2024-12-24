using System.ComponentModel.DataAnnotations.Schema;
using UserService.Domain.Constants;

namespace UserService.Domain.Entities;

[Table("Setting")]
public class Setting : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
    public SettingDataType DataType { get; set; }
}
