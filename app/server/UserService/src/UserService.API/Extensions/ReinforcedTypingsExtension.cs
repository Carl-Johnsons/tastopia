using Contract.DTOs.RecipeDTO;
using Contract.Extension;
using MongoDB.Driver;
using Reinforced.Typings.Fluent;
using UserService.API.DTOs;
using UserService.Domain.Entities;
using UserService.Domain.Errors;
using UserService.Domain.Responses;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
namespace UserService.API.Extensions;

// UserService.API.Extensions.ReinforcedTypingsExtension.ConfigureReinforcedTypings
public static class ReinforcedTypingsExtension
{
    private static string FILE_NAME = "user";
    private static List<string> EXPORT_FILE_PATHS = [
        "../../../../client/mobile/generated",
        "../../../../client/website/generated"
    ];

    public static void ConfigureReinforcedTypings(ConfigurationBuilder builder)
    {
        //Custom export file
        List<Type> errorsTypes = [
            typeof(SettingError),
            typeof(UserError)
        ];
        foreach (var path in EXPORT_FILE_PATHS)
        {
            Directory.CreateDirectory(path);
            builder.ConfigCommonReinforcedTypings(path, FILE_NAME, errorsTypes);
        }

        builder.GenerateTypesForMobileClient();
        builder.GenerateTypesForWebsiteClient();
    }
    private static void GenerateTypesForMobileClient(this ConfigurationBuilder builder)
    {
        // DTO and Entites
        builder.ExportAsInterfaces([
            typeof(SearchUserDTO),
            typeof(UpdateSettingDTO),
            typeof(SettingObjectDTO),
            typeof(UpdateUserDTO),
            typeof(FollowUserDTO),
            typeof(GetUserDetailByAccountIdDTO),
            typeof(GetUserFollowersDTO),
            typeof(GetUserFollowingsDTO),
            typeof(User),
            typeof(Setting),
            typeof(UserFollow),
            typeof(UserReport),
            typeof(UserSetting),
            typeof(FollowUserResponse),
            typeof(GetUserDetailsResponse),
            typeof(PaginatedSimpleUserListResponse),
            typeof(SimpleUserResponse),
            typeof(UserReportUserDTO),
            typeof(UserReportUserResponse),
            typeof(ReportReasonResponse),
            typeof(AdminGetUserDetailResponse),
        ], config =>
        {
            config.FlattenHierarchy()
                  .WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo($"mobile/generated/interfaces/{FILE_NAME}.interface.d.ts");
        });

        builder.ExportAsEnums([], config =>
        {
            config.FlattenHierarchy()
                  .DontIncludeToNamespace()
                  .UseString()
                  .ExportTo($"mobile/generated/enums/{FILE_NAME}.enum.ts");
        });
    }

    private static void GenerateTypesForWebsiteClient(this ConfigurationBuilder builder)
    {
        // DTO and Entites
        builder.ExportAsInterfaces([
            typeof(AdminGetUserDetailResponse),
            typeof(GetUserDetailByAccountIdDTO),
            typeof(Setting),
            typeof(UserSetting),
        ], config =>
        {
            config.FlattenHierarchy()
                  .WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo($"website/generated/interfaces/{FILE_NAME}.interface.d.ts");
        });

        builder.ExportAsEnums([], config =>
        {
            config.FlattenHierarchy()
                  .DontIncludeToNamespace()
                  .UseString()
                  .ExportTo($"website/generated/enums/{FILE_NAME}.enum.ts");
        });
    }
}
