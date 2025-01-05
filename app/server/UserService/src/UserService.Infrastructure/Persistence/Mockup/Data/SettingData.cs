using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence.Mockup.Data;

internal class SettingData
{
    public static List<Setting> Data = [
            new Setting {
                Id = Guid.Parse("5fad2a82-82db-430b-b61b-13704a91944a"),
                Code = SETTING_KEY.LANGUAGE.ToString(),
                DataType = SettingDataType.String,
                Description = "Language for display data for website dashboard and mobile app, default is English",
                DefaultValue = SETTING_VALUE.LANGUAGE.ENGLISH.ToString()
            },
            new Setting {
                Id = Guid.Parse("dad92eec-123c-4ae2-9848-b7f7a1a6ed56"),
                Code = SETTING_KEY.DARK_MODE.ToString(),
                DataType = SettingDataType.Boolean,
                Description = "Theme for website dashboard and mobile app, default is false (which is light mode)",
                DefaultValue = SETTING_VALUE.BOOLEAN.TRUE.ToString()
            },
            new Setting {
                Id = Guid.Parse("7744726e-2ad1-4f2f-bdac-cca718ef4100"),
                Code = SETTING_KEY.NOTIFICATION_COMMENT.ToString(),
                DataType = SettingDataType.Boolean,
                Description = "This setting can be disable if the user don't want to be receive notifications from comments",
                DefaultValue = SETTING_VALUE.BOOLEAN.TRUE.ToString()
            },
            new Setting {
                Id = Guid.Parse("7e3ea328-89c7-436c-ba60-02ae28d1d11c"),
                Code = SETTING_KEY.NOTIFICATION_FOLLOW.ToString(),
                DataType = SettingDataType.Boolean,
                Description = "This setting can be disable if the user don't want to be receive notifications from follow",
                DefaultValue = SETTING_VALUE.BOOLEAN.TRUE.ToString()
            },
            new Setting {
                Id = Guid.Parse("0068f002-8251-4051-b335-62685ed9d02a"),
                Code = SETTING_KEY.NOTIFICATION_VOTE.ToString(),
                DataType = SettingDataType.Boolean,
                Description = "This setting can be disable if the user don't want to be receive notifications from votes",
                DefaultValue = SETTING_VALUE.BOOLEAN.TRUE.ToString()
            }
        ];
}
