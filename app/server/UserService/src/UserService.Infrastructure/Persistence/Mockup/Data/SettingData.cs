using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence.Mockup.Data;

internal class SettingData
{
    public static List<Setting> Data = [
            new Setting { 
                Id = Guid.Parse("5fad2a82-82db-430b-b61b-13704a91944a"),
                Code = "LANGUAGE",
                DataType = SettingDataType.String,
                Description = "Language for display data for website dashboard and mobile app",
            },
            new Setting {
                Id = Guid.Parse("dad92eec-123c-4ae2-9848-b7f7a1a6ed56"),
                Code = "THEME",
                DataType = SettingDataType.String,
                Description = "Theme for website dashboard and mobile app",
            }
        ];
}
