using RecipeWorker;
using RecipeWorker.Extensions;
using RecipeWorker.Interfaces;
using RecipeWorker.Services;
using RecipeWorker.Utilities;

EnvUtility.LoadEnvFile();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureKestrel();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddWorkerServices();
        services.AddTransient<IRecipeService, CommunityRecipeService>();
        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();
