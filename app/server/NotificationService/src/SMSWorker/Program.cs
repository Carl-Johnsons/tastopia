using Contract.Extension;
using Contract.Utilities;
using SMSWorker;
using SMSWorker.Interfaces;
using SMSWorker.Services;

EnvUtility.LoadEnvFile();

const string serviceName = "SMSWorker";

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureLoggingService(serviceName)
    .ConfigureServices((context, services) =>
    {
        services.AddWorkerServices(serviceName);
        services.AddTransient<ISMSService, SMSService>();
        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();
