using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

DotNetEnv.Env.Load();

var reactUrl = Environment.GetEnvironmentVariable("REACT_URL") ?? "http://localhost:3000";

var builder = new WebHostBuilder();
builder.UseKestrel()
       .UseContentRoot(Directory.GetCurrentDirectory())
       .ConfigureAppConfiguration((hostingContext, config) =>
       {
           var env = hostingContext.HostingEnvironment;

           Console.WriteLine("Environment name: " + env.EnvironmentName);

           config.
               SetBasePath(env.ContentRootPath)
               //.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
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

       });
// Add logging
builder.ConfigureLogging(options =>
{
    options.AddConsole();
});

// Configure service
builder.ConfigureServices(services =>
{
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
});

// Start
builder.Configure(app =>
{
    app.UseCors("AllowAnyOriginPolicy");

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseOcelot().Wait();
})
.Build()
.Run();
