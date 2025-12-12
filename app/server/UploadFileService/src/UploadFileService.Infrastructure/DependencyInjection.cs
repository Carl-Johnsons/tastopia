using Contract.Extension;
using Microsoft.Extensions.DependencyInjection;
using UploadFileService.Infrastructure.Utilities;

namespace UploadFileService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IFileUtility), typeof(FileUtility));
        services.AddCommonInfrastructureServices("UploadFileService.API");
        return services;
    }
}
