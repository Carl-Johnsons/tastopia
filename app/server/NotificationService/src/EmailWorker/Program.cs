using EmailWorker;
using EmailWorker.Interfaces;
using EmailWorker.Services;
using EmailWorker.Utilities;
using NotificationService.Infrastructure;

EnvUtility.LoadEnvFile();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddWorkerServices();
        services.AddTransient<IEmailService, GmailEmailService>();
        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();
