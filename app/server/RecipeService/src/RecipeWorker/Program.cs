using Contract.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using RecipeWorker;
using RecipeWorker.Extensions;

const string serviceName = "RecipeWorker";

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureLoggingService(serviceName)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureKestrel();
        webBuilder.ConfigureServices(services =>
        {
            services.AddWorkerServices(serviceName);
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
