using Contract.Constants;
using Contract.Extension;
using RecipeService.API.DTOs;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using Reinforced.Typings.Fluent;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
namespace RecipeService.API.Extensions;

// RecipeService.API.Extensions.ReinforcedTypingsExtension.ConfigureReinforcedTypings
public static class ReinforcedTypingsExtension
{
    private static string FILE_NAME = "recipe";
    private static List<string> EXPORT_FILE_PATHS = [
        "../../../../client/mobile/generated",
        "../../../../client/website/src/generated"
    ];

    public static void ConfigureReinforcedTypings(ConfigurationBuilder builder)
    {
        // Custom export file
        List<Type> errorsTypes = [
            typeof(CommentError),
            typeof(RecipeError),
            typeof(TagError)
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
            typeof(CommentRecipeDTO),
            typeof(CreateRecipeDTO),
            typeof(StepDTO),
            typeof(UpdateRecipeDTO),
            typeof(UpdateStepDTO),
            typeof(DeleteOwnRecipeDTO),
            typeof(GetRecipeCommentsDTO),
            typeof(GetRecipeDetailDTO),
            typeof(GetRecipeFeedsDTO),
            typeof(GetTagsDTO),
            typeof(SearchRecipesDTO),
            typeof(VoteRecipeDTO),
            typeof(CommentVote),
            typeof(Recipe),
            typeof(Step),
            typeof(Comment),
            typeof(RecipeVote),
            typeof(RecipeTag),
            typeof(Tag),
            typeof(UserBookmarkRecipe),
            typeof(UserReportComment),
            typeof(UserReportRecipe),
            typeof(BookmarkRecipeDTO),
            typeof(GetRecipeBookmarkDTO),
            typeof(GetRecipeStepsDTO),
            typeof(GetAccountRecipeCommentsDTO),
            typeof(AccountRecipeCommentResponse),
            typeof(UserReportRecipeDTO),
            typeof(UserReportRecipeResponse),
            typeof(UserReportCommentDTO),
            typeof(UserReportCommentResponse),
            typeof(ReportReasonResponse),
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
            typeof(Tag),
            typeof(Comment),
            typeof(Step),
            typeof(RecipeVote),
            typeof(CommentVote),
            typeof(RecipeTag),
            typeof(Recipe),
            typeof(ReportDTO),
            typeof(EntityIdDTO),
            typeof(AccountRecipeCommentResponse),
            typeof(NumberedPaginatedMetadata),
            typeof(PaginatedAccountRecipeCommentListResponse),
            typeof(AdminRecipeResponse),
            typeof(UserActivityResponse),
            typeof(AdminGetUserActivityDTO),
            typeof(PaginatedUserActivityListResponse),
            typeof(AdminReportRecipeResponse),
            typeof(PaginatedAdminReportRecipeListResponse),
            typeof(ReportRecipeResponse),
            typeof(AdminReportRecipeDetailResponse),
            typeof(AdvancePaginatedMetadata),
            typeof(RecipeCommentResponse),
            typeof(PaginatedRecipeCommentListResponse),
        ], config =>
        {
            config.FlattenHierarchy()
                  .WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo($"website/src/generated/interfaces/{FILE_NAME}.interface.d.ts");
        });

        builder.ExportAsEnums([
            typeof(ReportType),
            typeof(ReportStatus)
        ], config =>
        {
            config.FlattenHierarchy()
                  .DontIncludeToNamespace()
                  .UseString()
                  .ExportTo($"website/src/generated/enums/{FILE_NAME}.enum.ts");
        });
    }
}
