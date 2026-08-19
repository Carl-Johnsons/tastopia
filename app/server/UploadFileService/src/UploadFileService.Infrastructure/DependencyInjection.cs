using Contract.Extension;
using Contract.Interfaces;
using Contract.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace UploadFileService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        => services.AddInternalInfrastructureServices()
                   .AddExternalInfrastructureServices();

    private static IServiceCollection AddInternalInfrastructureServices(this IServiceCollection services)
    {
        // MediatR require repository scope dependency injection
        services.AddScoped(typeof(IFileUtility), typeof(Utilities.FileUtility));
        services.AddScoped(typeof(IPaginateDataUtility<,>), typeof(PaginateDataUtility<,>));

        return services;
    }

    private static IServiceCollection AddExternalInfrastructureServices(this IServiceCollection services)
        => services.AddServiceDiscoveryService()
                   .AddMessagingService("UploadFileService.API");


    public static WebApplication UseInfrastructureServices(this WebApplication app)
        => app.UseLoggingServices()
               .UseServiceDiscoveryService(DotNetEnv.Env.GetString("CONSUL_UPLOAD", "Not Found"));
}
