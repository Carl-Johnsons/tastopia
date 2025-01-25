using Contract.Extension;
using Microsoft.Extensions.DependencyInjection;
using UploadFileService.Infrastructure.Persistence;
using UploadFileService.Infrastructure.Utilities;

namespace UploadFileService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IFileUtility), typeof(FileUtility));
        services.AddCommonInfrastructureServices("UploadFileService.API");
        return services;
    }
}
