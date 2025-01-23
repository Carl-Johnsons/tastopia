using Contract.Utilities;
using EmailWorker;
using EmailWorker.Extensions;
using EmailWorker.Interfaces;
using EmailWorker.Services;

EnvUtility.LoadEnvFile();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureSerilog()
    .ConfigureServices((context, services) =>
    {
        services.AddWorkerServices();
        services.AddTransient<IEmailService, GmailEmailService>();
        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();
