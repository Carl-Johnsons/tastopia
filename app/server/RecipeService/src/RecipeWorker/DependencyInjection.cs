
using Consul;
using Contract.Common;
using MassTransit;
using RecipeProto;
using RecipeWorker.EventPublishing;
using RecipeWorker.Interfaces;
using RecipeWorker.Services;
using System.Reflection;

namespace RecipeWorker;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkerServices(this IServiceCollection services)
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
        services.AddScoped<IOffensiveTextCheckerService, OffensiveTextCheckerService>();
        services.AddGrpc();
        services.AddTransient<IRecipeService, CommunityRecipeService>();
        services.AddMassTransitService();
        services.AddGrpcClientService();

        return services;
    }

    private static void AddGrpcClientService(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var consulService = serviceProvider.GetRequiredService<IConsulRegistryService>();

        services.AddGrpcClient<GrpcRecipe.GrpcRecipeClient>(options =>
        {
            options.Address = consulService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_RECIPE", "Not Found"));
        });
    }

    private static IServiceCollection AddMassTransitService(this IServiceCollection services)
    {
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();

            var applicationAssembly = AppDomain.CurrentDomain.Load("RecipeWorker");
            busConfig.AddConsumers(applicationAssembly);

            busConfig.UsingRabbitMq((context, config) =>
            {
                var username = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER") ?? "admin";
                var password = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS") ?? "pass";
                var rabbitMQHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost:5672";

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
