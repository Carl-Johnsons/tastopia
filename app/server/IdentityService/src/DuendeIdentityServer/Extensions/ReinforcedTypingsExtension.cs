using IdentityService.Domain.Errors;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
namespace DuendeIdentityServer.Extensions;

// DuendeIdentityServer.Extensions.ReinforcedTypingsExtension.ConfigureReinforcedTypings
public static class ReinforcedTypingsExtension
{
    private static string FILE_NAME = "identity";

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
                  .ExportTo("interfaces\\common.interface.d.ts");
        });
        // DTO 
        builder.ExportAsInterfaces([
            typeof(LinkAccountDTO),
            typeof(RegisterAccountDTO),
            typeof(VerifyAccountDTO)
        ], config =>
        {
            config.WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo($"interfaces\\{FILE_NAME}.interface.d.ts");
        });

        // Custom export file
        List<Type> errorsTypes = [
            typeof(AccountError)
        ];

        GenerateTypescriptEnumFile(errorsTypes);
    }

    private static void GenerateTypescriptEnumFile(List<Type> errorsTypes)
    {
        var xmlDoc = XDocument.Load(Directory.GetCurrentDirectory() + "\\Reinforced.Typings.settings.xml");

        XNamespace msbuildNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";
        var exportFilePath = xmlDoc.Descendants(msbuildNamespace + "RtTargetDirectory")
                                   .FirstOrDefault()?.Value ?? "Not found";

        Directory.CreateDirectory($"{exportFilePath}\\enums");
        var disableWarning = @"/* eslint no-unused-vars: ""off"" */";
        var typescriptEnumString = disableWarning + "\n" + string.Join("\n", errorsTypes.Select(GenerateErrorEnumTypescript));

        File.WriteAllText($"{exportFilePath}\\enums\\{FILE_NAME}.enum.ts", typescriptEnumString);
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
