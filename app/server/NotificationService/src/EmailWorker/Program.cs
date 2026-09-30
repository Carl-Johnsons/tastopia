using Contract.Extension;
using Contract.Utilities;
using EmailWorker;
using EmailWorker.Interfaces;
using EmailWorker.Services;

EnvUtility.LoadEnvFile();

const string serviceName = "EmailWorker";

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureLoggingService(serviceName)
    .ConfigureServices((context, services) =>
    {
        services.AddWorkerServices(serviceName);
        services.AddTransient<IEmailService, GmailEmailService>();
        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();
