using Contract.Extension;
using IdentityService.Domain.Errors;
using Reinforced.Typings.Fluent;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
namespace DuendeIdentityServer.Extensions;

// DuendeIdentityServer.Extensions.ReinforcedTypingsExtension.ConfigureReinforcedTypings
public static class ReinforcedTypingsExtension
{
    private static string FILE_NAME = "identity";
    private static string EXPORT_FILE_PATH = "../../../../client/mobile/generated";

    public static void ConfigureReinforcedTypings(ConfigurationBuilder builder)
    {
        Directory.CreateDirectory(EXPORT_FILE_PATH);
        // Custom export file
        List<Type> errorsTypes = [
            typeof(AccountError)
        ];

        builder.ConfigCommonReinforcedTypings(EXPORT_FILE_PATH, FILE_NAME, errorsTypes);

        // DTO and Entities
        builder.ExportAsInterfaces([
            typeof(AccountIdentifierDTO),
            typeof(RegisterAccountDTO),
            typeof(VerifyAccountDTO),
            typeof(ApplicationAccount),
            typeof(Group),
            typeof(Permission),
            typeof(RoleGroupPermission),
            typeof(CheckForgotPasswordDTO),
            typeof(ChangePasswordDTO)
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
