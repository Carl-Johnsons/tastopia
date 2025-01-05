using UserService.Application.Settings.Commands;

namespace UserService.API.DTOs;

public class UpdateSettingDTO
{
    public List<SettingObject> Settings { get; set; } = [];
}
