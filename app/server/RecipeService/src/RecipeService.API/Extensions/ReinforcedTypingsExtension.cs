using RecipeService.API.DTOs;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
namespace RecipeService.API.Extensions;

// RecipeService.API.Extensions.ReinforcedTypingsExtension.ConfigureReinforcedTypings
public static class ReinforcedTypingsExtension
{
    public static void ConfigureReinforcedTypings(ConfigurationBuilder builder)
    {
        builder.Global(config =>
        {
            config.CamelCaseForProperties()
                  .AutoOptionalProperties()
                  .ExportPureTypings(typings: true); 
        });

        // Substitute C# type to typescript type
        builder.Substitute(typeof(Guid), new RtSimpleTypeName("string"));

        // Common type
        builder.ExportAsInterfaces([
            typeof(ErrorResponseDTO)
        ], config =>
        {
            config.WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo("common.interface.d.ts");
        });
        // DTO 
        builder.ExportAsInterfaces([
            typeof(CommentRecipeDTO),
            typeof(CreateRecipeDTO),
            typeof(StepDTO),
            typeof(GetRecipeCommentsDTO),
            typeof(GetRecipeDetailDTO),
            typeof(GetRecipeFeedsDTO),
            typeof(GetTagsDTO),
            typeof(SearchRecipesDTO),
            typeof(VoteRecipeDTO)
        ], config =>
        {
            config.WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo("recipe.interface.d.ts");
        });
    }
}
