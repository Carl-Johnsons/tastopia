using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using UserService.API.DTOs;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
namespace UserService.API.Extensions;

// UserService.API.Extensions.ReinforcedTypingsExtension.ConfigureReinforcedTypings
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
            typeof(SearchUserDTO),
            typeof(UpdateSettingDTO),
            typeof(SettingObjectDTO),
            typeof(UpdateUserDTO)
        ], config =>
        {
            config.WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo("user.interface.d.ts");
        });
    }
}
