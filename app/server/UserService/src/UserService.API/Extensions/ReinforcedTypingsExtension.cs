using Contract.DTOs;
using Contract.Extension;
using RecipeService.API.DTOs;
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
    private static string EXPORT_FILE_PATH = "../../../../client/mobile/generated";

    public static void ConfigureReinforcedTypings(ConfigurationBuilder builder)
    {
        //Custom export file
        List<Type> errorsTypes = [
            typeof(SettingError),
            typeof(UserError),
            typeof(UserReportError)
        ];

        Directory.CreateDirectory(EXPORT_FILE_PATH);
        builder.ConfigCommonReinforcedTypings(EXPORT_FILE_PATH, FILE_NAME, errorsTypes);

        // DTO and Entites
        builder.ExportAsInterfaces([
            typeof(AdminMarkReportResponse),
            typeof(ReportDTO),
            typeof(PaginatedAdminUserReportDetailListResponse),
            typeof(PaginatedAdminUserReportListResponse),
            typeof(AdminUserReportDetailResponse),
            typeof(AdminUserReportResponse),
            typeof(AdminBanUserDTO),
            typeof(AdminBanUserResponse),
            typeof(AdminGetUserDetailResponse),
            typeof(AdminGetUserResponse),
            typeof(FollowUserDTO),
            typeof(FollowUserResponse),
            typeof(GetUserDetailByAccountIdDTO),
            typeof(GetUserDetailsResponse),
            typeof(GetUserFollowersDTO),
            typeof(GetUserFollowingsDTO),
            typeof(PaginatedAdminGetUserListResponse),
            typeof(PaginatedDTO),
            typeof(PaginatedSimpleUserListResponse),
            typeof(ReportReasonResponse),
            typeof(SearchUserDTO),
            typeof(Setting),
            typeof(SettingObjectDTO),
            typeof(SimpleUserResponse),
            typeof(UpdateSettingDTO),
            typeof(UpdateUserDTO),
            typeof(User),
            typeof(UserFollow),
            typeof(UserReport),
            typeof(UserReportUserDTO),
            typeof(UserReportUserResponse),
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
