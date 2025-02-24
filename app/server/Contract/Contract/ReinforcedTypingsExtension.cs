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
        Directory.CreateDirectory(EXPORT_FILE_PATH);

        //Custom export file
        List<Type> errorsTypes = [];

        builder.ConfigCommonReinforcedTypings(EXPORT_FILE_PATH, FILE_NAME, errorsTypes);
        // Common type
        builder.ExportAsInterfaces([
            typeof(ErrorResponseDTO),
            typeof(AdvancePaginatedMetadata),
            typeof(CommonPaginatedMetadata),
        ], config =>
        {
            config.WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo("interfaces/common.interface.d.ts");
        });
    }
}
