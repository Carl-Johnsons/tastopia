using EmailWorker;
using EmailWorker.Interfaces;
using EmailWorker.Services;
using EmailWorker.Utilities;

EnvUtility.LoadEnvFile();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<IEmailService, GmailEmailService>();

        services.AddSingleton(provider => new RabbitMQProvider(new RabbitMQ.Client.ConnectionFactory
        {
            HostName = DotNetEnv.Env.GetString("RABBITMQ_HOST", "localhost"),
            Port = 5672,
            UserName = DotNetEnv.Env.GetString("RABBITMQ_DEFAULT_USER", "admin"),
            Password = DotNetEnv.Env.GetString("RABBITMQ_DEFAULT_PASS", "pass"),
            VirtualHost = DotNetEnv.Env.GetString("RABBITMQ_DEFAULT_VHOST", "/"),
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
        }, provider.GetRequiredService<ILogger<RabbitMQProvider>>()));

        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();
