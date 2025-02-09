using Contract.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using PushNotificationWorker;
using PushNotificationWorker.Extensions;

EnvUtility.LoadEnvFile();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureSerilog()
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureKestrel();
        webBuilder.ConfigureServices(services =>
        {
            services.AddWorkerServices();
        });
        webBuilder.Configure(app =>
        {
            app.UseRouting();
        });
    })
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();
