namespace Contract.DTOs.UserDTO;

public class UserSettingDTO
{
    public string SettingId { get; set; } = null!;
    public string SettingCode { get; set; } = null!;
    public string SettingType { get; set; } = null!;
    public string SettingValue { get; set; } = null!;
    public string DefaultValue { get; set; } = null!;
}
