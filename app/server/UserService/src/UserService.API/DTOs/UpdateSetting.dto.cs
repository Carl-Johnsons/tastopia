namespace UserService.API.DTOs;

public class UpdateSettingDTO
{
    public List<SettingObjectDTO> Settings { get; set; } = [];
}

public class SettingObjectDTO
{
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
}
