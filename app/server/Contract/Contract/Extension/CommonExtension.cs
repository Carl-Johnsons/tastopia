using Consul;
using Contract.Interfaces;
using Contract.Services;
using Microsoft.Extensions.DependencyInjection;
using Contract.Utilities;
using Contract.Common;
using MassTransit;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Serilog;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Contract.DTOs;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using System.Text;

namespace Contract.Extension;

public static class CommonExtension
{
    public static WebApplicationBuilder ConfigureCommonAPIServices(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();

        builder.ConfigureSerilog();
        builder.ConfigureKestrel();
        builder.ConfigureHealthCheck();
        return builder;
    }

    /**
     * <summary>
     *   Add ErrorValidation, Controller, HttpContextAccessor and Authentication
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

        services.AddCustomAuthentication();

        return services;
    }

    /**
     * <summary>
     *   Add Consul, MassTransit and PaginateDataUtility
     * </summary>
     */
    public static IServiceCollection AddCommonInfrastructureServices(this IServiceCollection services, string apiPrjName)
    {
        services.AddScoped(typeof(IPaginateDataUtility<,>), typeof(PaginateDataUtility<,>));
        services.AddSingleton<IConsulClient, ConsulClient>(serviceProvider =>
        {
            return new ConsulClient(config =>
            {
                var scheme = DotNetEnv.Env.GetString("CONSUL_SCHEME", "Not found");
                var host = DotNetEnv.Env.GetString("CONSUL_HOST", "Not found");
                var port = DotNetEnv.Env.GetString("CONSUL_PORT", "Not found");
                config.Address = new Uri($"{scheme}://{host}:{port}");
            });
        });
        services.AddSingleton<IConsulRegistryService, ConsulRegistryService>();
        services.AddMassTransitService(apiPrjName);

        return services;
    }

    /**
     * <summary>
     *   Add ConsulRegistryService for service discovery, only usable for api gateway
     * </summary>
     */
    public static IServiceCollection AddAPIGatewayInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IConsulClient, ConsulClient>(serviceProvider =>
        {
            return new ConsulClient(config =>
            {
                var scheme = DotNetEnv.Env.GetString("CONSUL_SCHEME", "Not found");
                var host = DotNetEnv.Env.GetString("CONSUL_HOST", "Not found");
                var port = DotNetEnv.Env.GetString("CONSUL_PORT", "Not found");
                config.Address = new Uri($"{scheme}://{host}:{port}");
            });
        });
        services.AddSingleton<IConsulRegistryService, ConsulRegistryService>();

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
        var retryPolicy = Polly.Policy.Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 20,
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
            });
        return services;
    }

    private static IServiceCollection AddMassTransitService(this IServiceCollection services, string apiPrjName)
    {
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();

            var applicationAssembly = AppDomain.CurrentDomain.Load(apiPrjName);
            busConfig.AddConsumers(applicationAssembly);

            busConfig.UsingRabbitMq((context, config) =>
            {
                var username = DotNetEnv.Env.GetString("RABBITMQ_DEFAULT_USER", "admin");
                var password = DotNetEnv.Env.GetString("RABBITMQ_DEFAULT_PASS", "pass");
                var rabbitMQHost = DotNetEnv.Env.GetString("RABBITMQ_HOST", "localhost:5672");

                config.Host(new Uri($"amqp://{rabbitMQHost}/"), h =>
                {
                    h.Username(username);
                    h.Password(password);

                    h.Heartbeat(TimeSpan.FromSeconds(10));
                });

                config.UseMessageRetry(retryConfig =>
                {
                    retryConfig.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
                });

                RegisterEndpointsFromAttributes(context, config, applicationAssembly);

                config.ConfigureEndpoints(context);
            });
        });
        services.AddScoped<IServiceBus, MassTransitServiceBus>();
        return services;
    }

    private static void RegisterEndpointsFromAttributes(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator config, Assembly assembly)
    {
        var consumerTypes = assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConsumer<>)));

        foreach (var consumerType in consumerTypes)
        {
            var queueNameAttribute = consumerType.GetCustomAttribute<QueueNameAttribute>();
            if (queueNameAttribute == null)
            {
                continue;
            }
            config.ReceiveEndpoint(queueNameAttribute.QueueName, endpoint =>
            {
                endpoint.ConfigureConsumer(context, consumerType);

                endpoint.Bind(queueNameAttribute.ExchangeName);
            });
        }
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
                  .UseModules();
        });

        // Substitute C# type to typescript type
        builder.Substitute(typeof(Guid), new RtSimpleTypeName("string"));
        builder.Substitute(typeof(DateTime), new RtSimpleTypeName("string"));

        // Common type
        builder.ExportAsInterfaces([
            typeof(ErrorResponseDTO),
            typeof(AdvancePaginatedMetadata),
            typeof(CommonPaginatedMetadata),
        ], config =>
        {
            config.WithPublicProperties()
                  .AutoI()
                  .DontIncludeToNamespace()
                  .ExportTo("interfaces/common.interface.d.ts");
        });

        // Custom export file
        GenerateTypescriptEnumFile(errorTypes, exportFilePath, fileName);
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
