using System.Reflection;
using Contract.Common;
using Contract.Interfaces;
using Contract.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Contract.Extension;

public static class MessagingExtension
{
    public static IServiceCollection AddMessagingService(this IServiceCollection services, string apiPrjName)
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
}