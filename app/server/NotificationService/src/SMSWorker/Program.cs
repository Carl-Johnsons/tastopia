using Contract.Utilities;
using SMSWorker;
using SMSWorker.Extensions;
using SMSWorker.Interfaces;
using SMSWorker.Services;

EnvUtility.LoadEnvFile();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureSerilog()
    .ConfigureServices((context, services) =>
    {
        services.AddWorkerServices();
        services.AddTransient<ISMSService, SMSService>();
        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();
