﻿using Contract.Common;
using Contract.Constants;
using Contract.DTOs;
using Contract.Extension;
using Contract.Interfaces;
using Reinforced.Typings.Fluent;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;
namespace Contract;

// Contract.ReinforcedTypingsExtension.ConfigureReinforcedTypings
public static class ReinforcedTypingsExtension
{
    private static string FILE_NAME = "common";
    private static string EXPORT_FILE_PATH = "../../../client/mobile/generated";

    public static void ConfigureReinforcedTypings(ConfigurationBuilder builder)
    {
        // Custom export file
        List<Type> errorsTypes = [];

        Directory.CreateDirectory(EXPORT_FILE_PATH);
        builder.ConfigContractReinforcedTypings(EXPORT_FILE_PATH, FILE_NAME, errorsTypes);

        // DTO and Entites
        builder.ExportAsInterfaces([
            typeof(ErrorResponseDTO),
            typeof(AdvancePaginatedMetadata),
            typeof(CommonPaginatedMetadata),
            typeof(NumberedPaginatedMetadata),
            typeof(PaginateParam)
        ], config =>
        {
            config.FlattenHierarchy()
                  .WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo($"interfaces/{FILE_NAME}.interface.d.ts");
        });

        builder.ExportAsEnums([
            typeof(SortType),
            typeof(ActivityEntityType),
            typeof(ActivityType)
        ], config =>
        {
            config.FlattenHierarchy()
                  .DontIncludeToNamespace()
                  .UseString()
                  .ExportTo($"enums/{FILE_NAME}.enum.ts");
        });
    }
}
