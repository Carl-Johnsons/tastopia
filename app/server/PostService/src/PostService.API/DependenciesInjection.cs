using PostService.Application;
using PostService.Infrastructure;
using PostService.API.Middleware;
using Microsoft.IdentityModel.Tokens;
using PostService.Domain.Interfaces;

namespace PostService.API;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public static class DependenciesInjection
{
    public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder)
    {
        builder.Logging.AddConsole();

        var services = builder.Services;
        var config = builder.Configuration;

        services.AddApplicationServices();
        services.AddInfrastructureServices();
        services.AddHttpContextAccessor();

        services.AddControllers();


        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                var IdentityDNS = (Environment.GetEnvironmentVariable("IDENTITY_SERVER_HOST") ?? "localhost:5001").Replace("\"", "");
                var IdentityServerEndpoint = $"http://{IdentityDNS}";
                Console.WriteLine("Connect to Identity Provider: " + IdentityServerEndpoint);

                options.Authority = IdentityServerEndpoint;
                options.RequireHttpsMetadata = false;
                // Clear default Microsoft's JWT claim mapping
                // Ref: https://stackoverflow.com/questions/70766577/asp-net-core-jwt-token-is-transformed-after-authentication
                options.MapInboundClaims = false;
                options.TokenValidationParameters.ValidTypes = ["at+jwt"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    RoleClaimType = "role"
                };
                // For development only
                options.IncludeErrorDetails = true;
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return builder;
    }

    public static async Task<WebApplication> UseAPIServicesAsync(this WebApplication app)
    {

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseGlobalHandlingErrorMiddleware();

        app.MapControllers();

        try
        {
            var signalRService = app.Services.GetService<ISignalRService>();
            await signalRService!.StartConnectionAsync();
        }
        catch (Exception ex)
        {
            app.Logger.LogError($"Error connecting to SignalR: {ex.Message}");
        }
        return app;
    }
}

