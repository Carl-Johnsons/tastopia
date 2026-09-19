using Contract.Common;
using Contract.DTOs;
using Contract.Interfaces;
using Contract.Middleware;
using Contract.Utilities;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Polly;
using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using Reinforced.Typings.Generators;
using Reinforced.Typings.Visitors.TypeScript;
using Serilog;
using System.Reflection;
using System.Text;

namespace Contract.Extension;

public static class CommonExtension
{
    /**
     * <summary>
     *   Add ErrorValidation, Controller and HttpContextAccessor
     * </summary>
     */
    public static IServiceCollection AddCommonAPIServices(this IServiceCollection services)
    {
        services.AddErrorValidation();
        services.AddControllers()
            // Prevent circular JSON reach max depth of the object when serialization
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            //    options.JsonSerializerOptions.WriteIndented = true;
            //})
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;
            });

        services.AddHttpContextAccessor();

        return services;
    }

    /**
     * <summary>
     *   Add Auth, only usable for api gateway
     * </summary>
     */
    public static IServiceCollection AddAPIGatewayAPIServices(this IServiceCollection services)
    {
        EnvUtility.LoadEnvFile();
        services.AddCustomAuthentication();

        return services;
    }

    private static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
    {
        var retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 100,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(attempt),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    // Log the retry attempt
                    Log.Warning($"Retry {retryCount} encountered an error: {exception.Message}. Waiting {timeSpan} before next retry.");
                });

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var consulRegistryService = serviceProvider.GetRequiredService<IConsulRegistryService>();
                var identityUri = retryPolicy.ExecuteAsync(() =>
                {
                    var uri = consulRegistryService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_IDENTITY", "Not found"));
                    return uri == null ? throw new Exception("Identity service URI not found.") : Task.FromResult(uri);
                }).GetAwaiter().GetResult();

                Log.Information("Connect to Identity Provider: " + identityUri!.ToString());

                options.RequireHttpsMetadata = false;
                options.Authority = identityUri!.ToString();
                // Clear default Microsoft's JWT claim mapping
                // Ref: https://stackoverflow.com/questions/70766577/asp-net-core-jwt-token-is-transformed-after-authentication
                options.MapInboundClaims = false;

                options.TokenValidationParameters.ValidTypes = ["at+jwt"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                // For development only
                options.IncludeErrorDetails = true;

                options.BackchannelHttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var logger = context.HttpContext.RequestServices
                            .GetRequiredService<ILoggerFactory>()
                            .CreateLogger("JwtBearer");
                        logger.LogInformation("Token validated successfully.");
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices
                            .GetRequiredService<ILoggerFactory>()
                            .CreateLogger("JwtBearer");
                        logger.LogError(context.Exception, "Token authentication failed.");
                        return Task.CompletedTask;
                    }
                };
            });
        return services;
    }

    /**
     * <summary>
     *  Authenticate for downstream service, ignore jwt validation because api gateway does all the heavy work
     * </summary>
     */
    public static IServiceCollection AddCustomDownstreamAuthentication(this IServiceCollection services)
    {

        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            // Clear default Microsoft's JWT claim mapping
            // Ref: https://stackoverflow.com/questions/70766577/asp-net-core-jwt-token-is-transformed-after-authentication
            options.MapInboundClaims = false;
            options.SaveToken = true;

            // Completely disable token validations
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = false,
                RequireSignedTokens = false,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
                /*  
                 *  Return JwtSecurityToken(token) will cause this error
                 *  
                 *  Microsoft.IdentityModel.Tokens.SecurityTokenInvalidSignatureException: IDX10506: Signature validation failed.
                 *  The user defined 'Delegate' specified on TokenValidationParameters did not return a 'Microsoft.IdentityModel.JsonWebTokens.JsonWebToken',
                 *  but returned a 'System.IdentityModel.Tokens.Jwt.JwtSecurityToken' when validating token: '[PII of type 'Microsoft.IdentityModel.JsonWebTokens.
                 *  JsonWebToken' is hidden. For more details, see https://aka.ms/IdentityModel/PII.]'
                */
                SignatureValidator = (token, parameters) => new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(token)
            };
            // For development only
            options.IncludeErrorDetails = true;
        });
        return services;
    }

    /// <summary>
    ///  Use middleware for global error handling and request validation, use before UseRouting() and UseAuthentication()
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication UseCommonAPIMiddleware(this WebApplication app)
    {
        app.UseMiddleware<GlobalHandlingErrorMiddleware>()
           .UseMiddleware<ValidateGatewayRequestMiddleware>();
        return app;
    }

    // ================ Config reinforcedTyping =================
    internal static void ConfigContractReinforcedTypings(this Reinforced.Typings.Fluent.ConfigurationBuilder builder,
                                                 string exportFilePath,
                                                 string fileName,
                                                 List<Type> errorTypes)
    {
        builder.Global(config =>
        {
            config.CamelCaseForProperties()
                  .AutoOptionalProperties()
                  .ExportPureTypings(typings: true)
                  .GenerateDocumentation()
                  .UseModules();
        });

        // Substitute C# type to typescript type
        builder.Substitute(typeof(Guid), new RtSimpleTypeName("string"));
        builder.Substitute(typeof(DateTime), new RtSimpleTypeName("string"));

        // Custom export file
        GenerateTypescriptEnumFile(errorTypes, exportFilePath, fileName);
    }

    public static void ConfigCommonReinforcedTypings(this Reinforced.Typings.Fluent.ConfigurationBuilder builder,
                                                     string exportFilePath,
                                                     string fileName,
                                                     List<Type> errorTypes)
    {
        builder.Global(config =>
        {
            config.CamelCaseForProperties()
                  .AutoOptionalProperties()
                  .ExportPureTypings(typings: true)
                  .GenerateDocumentation()
                  .UseVisitor<CustomExportVisitor>()
                  .UseModules();
        });

        // Substitute C# type to typescript type
        builder.Substitute(typeof(Guid), new RtSimpleTypeName("string"));
        builder.Substitute(typeof(DateTime), new RtSimpleTypeName("string"));
        builder.Substitute(typeof(ErrorResponseDTO), new RtSimpleTypeName("IErrorResponseDTO"));
        builder.Substitute(typeof(AdvancePaginatedMetadata), new RtSimpleTypeName("IAdvancePaginatedMetadata"));
        builder.Substitute(typeof(CommonPaginatedMetadata), new RtSimpleTypeName("ICommonPaginatedMetadata"));
        builder.Substitute(typeof(NumberedPaginatedMetadata), new RtSimpleTypeName("INumberedPaginatedMetadata"));

        // Custom export file
        GenerateTypescriptEnumFile(errorTypes, exportFilePath, fileName);
    }

    private class CustomExportVisitor : TypeScriptExportVisitor
    {
        public CustomExportVisitor(TextWriter writer, ExportContext exportContext) : base(writer, exportContext)
        {
        }

        public override void VisitFile(ExportedFile file)
        {
            if (file.FileName.EndsWith(".interface.d.ts", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("// Generated at: " + DateTime.Now);
                WriteLine(@"
import {
    IErrorResponseDTO,
    IAdvancePaginatedMetadata,
    ICommonPaginatedMetadata,
    INumberedPaginatedMetadata
} from ""./common.interface"";

import {
    ActivityType,
    ActivityEntityType,
    SortType
} from ""../enums/common.enum"";
            ");
            }

            // Continue processing the rest of the file normally
            base.VisitFile(file);
        }
    }

    public class NamedImportGenerator : TsCodeGeneratorBase<string, RtRaw>
    {
        public override RtRaw GenerateNode(string memberName, RtRaw node, TypeResolver resolver)
        {
            // Generate a named import statement for the given member.
            return new RtRaw($"import {{ {memberName} }} from './common.interface.d.ts';");
        }
    }

    private static void GenerateTypescriptEnumFile(List<Type> errorsTypes, string exportFilePath, string fileName)
    {
        var enumsDirectory = Path.Combine(exportFilePath, "enums");
        Directory.CreateDirectory(enumsDirectory);
        var disableWarning = @"/* eslint no-unused-vars: ""off"" */";
        var typescriptEnumString = disableWarning + "\n" + string.Join("\n", errorsTypes.Select(GenerateErrorEnumTypescript));

        File.WriteAllText(Path.Combine(enumsDirectory, $"{fileName}.error.enum.ts"), typescriptEnumString);
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
