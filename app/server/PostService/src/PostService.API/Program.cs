using PostService.API;

var builder = WebApplication.CreateBuilder(args);

var app = builder.AddAPIServices()
                 .Build();

await app.UseAPIServicesAsync();

app.Run();
