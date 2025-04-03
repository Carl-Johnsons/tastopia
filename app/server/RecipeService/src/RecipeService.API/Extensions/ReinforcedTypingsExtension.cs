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
    private static string EXPORT_FILE_PATH = "../../../../client/mobile/generated";

    public static void ConfigureReinforcedTypings(ConfigurationBuilder builder)
    {
        // Custom export file
        List<Type> errorsTypes = [
            typeof(CommentError),
            typeof(RecipeError),
            typeof(TagError)
        ];

        Directory.CreateDirectory(EXPORT_FILE_PATH);
        builder.ConfigCommonReinforcedTypings(EXPORT_FILE_PATH, FILE_NAME, errorsTypes);

        // DTO and Entites
        builder.ExportAsInterfaces([
            typeof(DateStatisticEntity),
            typeof(MonthStatisticEntity),
            typeof(HourStatisticEntity),
            typeof(StatisticEntity),
            typeof(AdminGetTagDetailDTO),
            typeof(CreateTagDTO),
            typeof(UpdateTagDTO),
            typeof(AdminTagResponse),
            typeof(PaginatedAdminTagListResponse),
            typeof(AccountRecipeCommentResponse),
            typeof(AdminGetUserActivityDTO),
            typeof(AdminRecipeResponse),
            typeof(AdminReportRecipeDetailResponse),
            typeof(AdminReportRecipeResponse),
            typeof(BookmarkRecipeDTO),
            typeof(Comment),
            typeof(CommentRecipeDTO),
            typeof(CommentVote),
            typeof(CreateRecipeDTO),
            typeof(DeleteOwnRecipeDTO),
            typeof(EntityIdDTO),
            typeof(GetAccountRecipeCommentsDTO),
            typeof(GetRecipeBookmarkDTO),
            typeof(GetRecipeCommentsDTO),
            typeof(GetRecipeDetailDTO),
            typeof(GetRecipeFeedsDTO),
            typeof(GetRecipeStepsDTO),
            typeof(GetTagsDTO),
            typeof(PaginatedAccountRecipeCommentListResponse),
            typeof(PaginatedAdminReportRecipeListResponse),
            typeof(PaginatedRecipeCommentListResponse),
            typeof(PaginatedUserActivityListResponse),
            typeof(PaginatedTagListResponse),
            typeof(TagResponse),
            typeof(Recipe),
            typeof(RecipeCommentResponse),
            typeof(RecipeTag),
            typeof(RecipeVote),
            typeof(ReportDTO),
            typeof(ReportReasonResponse),
            typeof(ReportRecipeResponse),
            typeof(SearchRecipesDTO),
            typeof(Step),
            typeof(StepDTO),
            typeof(Tag),
            typeof(UpdateRecipeDTO),
            typeof(UpdateStepDTO),
            typeof(UserActivityResponse),
            typeof(UserBookmarkRecipe),
            typeof(UserReportComment),
            typeof(UserReportCommentDTO),
            typeof(UserReportCommentResponse),
            typeof(UserReportRecipe),
            typeof(UserReportRecipeDTO),
            typeof(UserReportRecipeResponse),
            typeof(VoteRecipeDTO),
            typeof(AdminReportCommentResponse),
            typeof(PaginatedAdminReportCommentListResponse),
            typeof(AdminReportCommentDetailResponse),
            typeof(CommentDetailResponse),
            typeof(PaginatedAdminRecipeListResponse)
        ], config =>
        {
            config.FlattenHierarchy()
                  .WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo($"interfaces/{FILE_NAME}.interface.d.ts");
        });

        builder.ExportAsEnums([
            typeof(ReportType),
            typeof(ReportStatus)
        ], config =>
        {
            config.FlattenHierarchy()
                  .DontIncludeToNamespace()
                  .UseString()
                  .ExportTo($"enums/{FILE_NAME}.enum.ts");
        });
    }
}
