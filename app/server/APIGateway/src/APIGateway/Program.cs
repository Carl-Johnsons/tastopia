using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

DotNetEnv.Env.Load();

var reactUrl = Environment.GetEnvironmentVariable("REACT_URL") ?? "http://localhost:3000";

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var services = builder.Services;
var env = builder.Environment;

config.SetBasePath(env.ContentRootPath)
      .AddEnvironmentVariables();

if (env.IsDevelopment())
{
    config.AddOcelot("Config/development", env);
}
else if (env.IsEnvironment("Kubernetes"))
{
    config.AddOcelot("Config/kubernetes", env);
}
else
{
    config.AddOcelot("Config/production", env);
}

services.AddLogging(options =>
{
    options.AddConsole();
});


// Configure service
services.AddEndpointsApiExplorer(); // This require for swagger to launch
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" });
});

services.AddOcelot();

services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var IdentityDNS = (Environment.GetEnvironmentVariable("IDENTITY_SERVER_HOST") ?? "localhost:5001").Replace("\"", "");
        var IdentityServerEndpoint = $"http://{IdentityDNS}";
        Console.WriteLine("Connect to Identity Provider: " + IdentityServerEndpoint);

        options.Authority = IdentityServerEndpoint;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            // Skip the validate issuer signing key
            //ValidateIssuerSigningKey = false,
            //SignatureValidator = delegate (string token, TokenValidationParameters parameters)
            //{
            //    var jwt = new JsonWebToken(token);

            //    return jwt;
            //},
            //ValidIssuers = [
            //    IdentityServerEndpoint
            //],
        };
        // For development only
        options.IncludeErrorDetails = true;
    });
services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOriginPolicy",
        builder =>
        {
            builder.WithOrigins(reactUrl)
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});
services.AddSignalR();

// Start
var app = builder.Build();

app.UseCors("AllowAnyOriginPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseOcelot().Wait();

app.Start();

var server = app.Services.GetService<IServer>();
var addresses = server?.Features.Get<IServerAddressesFeature>()?.Addresses;

if (addresses != null)
{
    foreach (var address in addresses)
    {
        Console.WriteLine($"API gateway is listening on: {address}");
    }
}
else
{
    Console.WriteLine("Could not retrieve server addresses.");
}

app.WaitForShutdown();