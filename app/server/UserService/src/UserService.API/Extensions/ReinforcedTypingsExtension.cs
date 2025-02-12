using Contract.Extension;
using Reinforced.Typings.Fluent;
using UserService.API.DTOs;
using UserService.Domain.Entities;
using UserService.Domain.Errors;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
namespace UserService.API.Extensions;

// UserService.API.Extensions.ReinforcedTypingsExtension.ConfigureReinforcedTypings
public static class ReinforcedTypingsExtension
{
    private static string FILE_NAME = "user";
    private static string EXPORT_FILE_PATH = "../../../../client/mobile/generated";

    public static void ConfigureReinforcedTypings(ConfigurationBuilder builder)
    {
        Directory.CreateDirectory(EXPORT_FILE_PATH);

        //Custom export file
        List<Type> errorsTypes = [
            typeof(SettingError),
            typeof(UserError)
        ];

        builder.ConfigCommonReinforcedTypings(EXPORT_FILE_PATH, FILE_NAME, errorsTypes);
        // DTO and entities
        builder.ExportAsInterfaces([
            typeof(SearchUserDTO),
            typeof(UpdateSettingDTO),
            typeof(SettingObjectDTO),
            typeof(UpdateUserDTO),
            typeof(User),
            typeof(Setting),
            typeof(UserFollow),
            typeof(UserReport),
            typeof(UserSetting),
        ], config =>
        {
            config.FlattenHierarchy()
                  .WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo($"interfaces/{FILE_NAME}.interface.d.ts");
        });

        builder.ExportAsEnums([], config =>
        {
            config.FlattenHierarchy()
                  .DontIncludeToNamespace()
                  .UseString()
                  .ExportTo($"enums/{FILE_NAME}.enum.ts");
        });
    }
}
