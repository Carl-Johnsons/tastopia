using Contract.Utilities;
using PushNotificationWorker;

EnvUtility.LoadEnvFile();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddWorkerServices();
        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();
