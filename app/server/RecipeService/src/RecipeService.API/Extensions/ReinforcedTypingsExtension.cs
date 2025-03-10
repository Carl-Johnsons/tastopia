using Contract.Extension;
using RecipeService.API.DTOs;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using Reinforced.Typings.Ast.TypeNames;
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
        Directory.CreateDirectory(EXPORT_FILE_PATH);

        // Custom export file
        List<Type> errorsTypes = [
            typeof(CommentError),
            typeof(RecipeError),
            typeof(TagError)
        ];

        builder.ConfigCommonReinforcedTypings(EXPORT_FILE_PATH, FILE_NAME, errorsTypes);

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
            //ADMIN SECTION
            typeof(AdminGetRecipesDTO),
            typeof(PaginatedAccountRecipeCommentListResponse),
            typeof(AdminRecipeResponse),
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
