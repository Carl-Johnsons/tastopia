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
    private static List<string> EXPORT_FILE_PATHS = [
        "../../../client/mobile/src/generated",
        "../../../client/website/src/generated"
    ];

    public static void ConfigureReinforcedTypings(ConfigurationBuilder builder)
    {
        // Custom export file
        List<Type> errorsTypes = [];

        foreach (var path in EXPORT_FILE_PATHS)
        {
            Directory.CreateDirectory(path);
            builder.ConfigCommonReinforcedTypings(path, FILE_NAME, errorsTypes);
        }
        // Common type
        builder.GenerateTypesForMobileClient();
        builder.GenerateTypesForWebsiteClient();
    }

    private static void GenerateTypesForMobileClient(this ConfigurationBuilder builder)
    {
        // DTO and Entites
        builder.ExportAsInterfaces([
            typeof(ErrorResponseDTO),
            typeof(AdvancePaginatedMetadata),
            typeof(CommonPaginatedMetadata)

        ], config =>
        {
            config.FlattenHierarchy()
                  .WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo($"mobile/src/generated/interfaces/{FILE_NAME}.interface.d.ts");
        });
    }

    private static void GenerateTypesForWebsiteClient(this ConfigurationBuilder builder)
    {
        // DTO and Entites
        builder.ExportAsInterfaces([
            typeof(ErrorResponseDTO),
            typeof(AdvancePaginatedMetadata),
            typeof(CommonPaginatedMetadata)
        ], config =>
        {
            config.FlattenHierarchy()
                  .WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo($"website/src/generated/interfaces/{FILE_NAME}.interface.d.ts");
        });
    }
}
