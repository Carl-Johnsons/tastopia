using UserService.Domain.Constants;

namespace UserService.Domain.Entities;

public class Setting : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
    public SettingDataType DataType { get; set; }
}
