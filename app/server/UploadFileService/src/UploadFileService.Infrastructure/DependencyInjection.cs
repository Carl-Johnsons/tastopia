using Consul;
using Contract.Common;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UploadFileService.Infrastructure.Persistence;
using UploadFileService.Infrastructure.Services;
using UploadFileService.Infrastructure.Utilities;

namespace UploadFileService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IFileUtility), typeof(FileUtility));
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
        services.AddMassTransitService();
        return services;
    }

    private static IServiceCollection AddMassTransitService(this IServiceCollection services)
    {
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();

            var applicationAssembly = AppDomain.CurrentDomain.Load("UploadFileService.API");
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
}
