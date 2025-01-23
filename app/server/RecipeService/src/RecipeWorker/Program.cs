using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using RecipeWorker;
using RecipeWorker.Extensions;

var builder = Host.CreateDefaultBuilder(args)
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
