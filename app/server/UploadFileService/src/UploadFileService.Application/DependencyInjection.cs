using CloudinaryDotNet;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UploadFileService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        DotNetEnv.Env.Load();
        services.AddSingleton(sp => {
            var cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("Cloudinary_URL"));
            cloudinary.Api.Secure = true;
            return cloudinary;
        });
        return services;
    }
}
