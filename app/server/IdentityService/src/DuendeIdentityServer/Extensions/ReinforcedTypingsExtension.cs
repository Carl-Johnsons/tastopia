using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
namespace DuendeIdentityServer.Extensions;

// DuendeIdentityServer.Extensions.ReinforcedTypingsExtension.ConfigureReinforcedTypings
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
            typeof(LinkAccountDTO),
            typeof(RegisterAccountDTO),
            typeof(VerifyAccountDTO)
        ], config =>
        {
            config.WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo("identity.interface.d.ts");
        });
    }
}
