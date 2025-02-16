using Contract.Constants;
using Contract.Extension;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using SubscriptionService.API.DTOs;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
namespace SubscriptionService.API.Extensions;

// SubscriptionService.API.Extensions.ReinforcedTypingsExtension.ConfigureReinforcedTypings
public static class ReinforcedTypingsExtension
{
    private static string FILE_NAME = "subscription";
    private static string EXPORT_FILE_PATH = "../../../../client/mobile/generated";

    public static void ConfigureReinforcedTypings(ConfigurationBuilder builder)
    {
        Directory.CreateDirectory(EXPORT_FILE_PATH);
        List<Type> errorsTypes = [
            //typeof(SettingError),
            //typeof(UserError)
        ];

        builder.ConfigCommonReinforcedTypings(EXPORT_FILE_PATH, FILE_NAME, errorsTypes);
        // DTO 
        builder.ExportAsInterfaces([
            //typeof(SearchUserDTO),
            //typeof(UpdateSettingDTO),
            //typeof(SettingObjectDTO),
            //typeof(UpdateUserDTO)
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
