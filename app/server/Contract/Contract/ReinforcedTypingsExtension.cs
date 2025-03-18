using Contract.Common;
using Contract.DTOs;
using Contract.Extension;
using Reinforced.Typings.Fluent;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
namespace Contract;

// Contract.ReinforcedTypingsExtension.ConfigureReinforcedTypings
public static class ReinforcedTypingsExtension
{
    private static string FILE_NAME = "common";
    private static string EXPORT_FILE_PATH = "../../../client/mobile/generated";

    public static void ConfigureReinforcedTypings(ConfigurationBuilder builder)
    {
        // Custom export file
        List<Type> errorsTypes = [];

        Directory.CreateDirectory(EXPORT_FILE_PATH);
        builder.ConfigContractReinforcedTypings(EXPORT_FILE_PATH, FILE_NAME, errorsTypes);

        // DTO and Entites
        builder.ExportAsInterfaces([
            typeof(ErrorResponseDTO),
            typeof(AdvancePaginatedMetadata),
            typeof(CommonPaginatedMetadata),
            typeof(NumberedPaginatedMetadata)

        ], config =>
        {
            config.FlattenHierarchy()
                  .WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo($"interfaces/{FILE_NAME}.interface.d.ts");
        });
    }
}
