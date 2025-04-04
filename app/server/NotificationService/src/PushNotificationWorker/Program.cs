using Contract.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using PushNotificationWorker;
using PushNotificationWorker.Extensions;

EnvUtility.LoadEnvFile();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureSerilog()
    .ConfigureServices((context, services) =>
    {
        services.AddWorkerServices();
        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();
