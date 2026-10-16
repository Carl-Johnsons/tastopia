using Contract.Extension;
using Contract.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using PushNotificationWorker;

EnvUtility.LoadEnvFile();

const string serviceName = "PushNotificationWorker";

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureLoggingService(serviceName)
    .ConfigureServices((context, services) =>
    {
        services.AddWorkerServices(serviceName);
        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();
