using RecipeService.API.DTOs;
using RecipeService.Domain.Errors;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
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
                  .ExportTo("interfaces/common.interface.d.ts");
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
                  .ExportTo($"interfaces/{FILE_NAME}.interface.d.ts");
        });

        // Custom export file
        List<Type> errorsTypes = [
            typeof(CommentError),
            typeof(RecipeError),
            typeof(TagError)
        ];

        GenerateTypescriptEnumFile(errorsTypes);
    }

    private static void GenerateTypescriptEnumFile(List<Type> errorsTypes)
    {
        var enumsDirectory = Path.Combine(EXPORT_FILE_PATH, "enums");
        Directory.CreateDirectory(enumsDirectory);
        var disableWarning = @"/* eslint no-unused-vars: ""off"" */";
        var typescriptEnumString = disableWarning + "\n" + string.Join("\n", errorsTypes.Select(GenerateErrorEnumTypescript));

        File.WriteAllText(Path.Combine(enumsDirectory, $"{FILE_NAME}.enum.ts"), typescriptEnumString);
    }

    private static string GenerateErrorEnumTypescript(Type errorType)
    {
        var errorDictionary = GetErrorsEnumValues(errorType);

        var sb = new StringBuilder();
        sb.AppendLine("export enum " + errorType.Name + " {");
        var lastIndex = errorDictionary.Count - 1;
        int currentIndex = 0;

        foreach (var (key, value) in errorDictionary)
        {
            if (currentIndex == lastIndex) sb.AppendLine($"\t{key} = \"{value}\"");
            else sb.AppendLine($"\t{key} = \"{value}\",");

            currentIndex++;
        }
        sb.AppendLine("}");

        return sb.ToString();
    }

    private static Dictionary<string, string> GetErrorsEnumValues(Type errorType)
    {
        return errorType.GetProperties(BindingFlags.Public | BindingFlags.Static)
                        .Where(p => p.PropertyType == typeof(Error))
                        .ToDictionary(
                            p => p.Name,
                            p => ((Error)p.GetValue(null)!).Code
                        );
    }
}
