using TrackingService.API.DTOs;
using Reinforced.Typings.Fluent;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
using Contract.Extension;
using TrackingService.Domain.Responses;
using Reinforced.Typings.Ast.TypeNames;
using Contract.Constants;
namespace TrackingService.API.Extensions;

// TrackingService.API.Extensions.ReinforcedTypingsExtension.ConfigureReinforcedTypings
public static class ReinforcedTypingsExtension
{
    private static string FILE_NAME = "tracking";
    private static string EXPORT_FILE_PATH = "../../../../client/mobile/generated";
    public static void ConfigureReinforcedTypings(ConfigurationBuilder builder)
    {
        List<Type> errorsTypes = [];

        builder.ConfigCommonReinforcedTypings(EXPORT_FILE_PATH, FILE_NAME, errorsTypes);
        builder.Substitute(typeof(ActivityType), new RtSimpleTypeName("ActivityType"));
        builder.Substitute(typeof(ActivityEntityType), new RtSimpleTypeName("ActivityEntityType"));
        // DTO 
        builder.ExportAsInterfaces([
            typeof(GetUserViewRecipeDetailHistoryDTO),
            typeof(SearchUserViewRecipeDetailHistoryDTO),
            typeof(AdminActivityLogResponse),
            typeof(PaginatedAdminActivityLogListResponse),
            typeof(RecipeAdminActivityLogResponse),
            typeof(RecipeLogResponse),
        ], config =>
        {
            config.FlattenHierarchy()
                  .WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo("interfaces/tracking.interface.d.ts");
        });
    }
}
