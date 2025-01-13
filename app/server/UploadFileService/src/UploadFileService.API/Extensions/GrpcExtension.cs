using Contract.Constants;
using Grpc.Net.Compression;
using System.IO.Compression;
using UploadFileService.API.GrpcServices;

namespace UploadFileService.API.Extensions;

public static class GrpcExtension
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddGrpc(options =>
        {
            options.MaxReceiveMessageSize = GrpcUploadFileConfig.MaxMessageSize;
            options.MaxSendMessageSize = GrpcUploadFileConfig.MaxMessageSize;
            options.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
            options.CompressionProviders = new[] { new GzipCompressionProvider(CompressionLevel.Optimal) };
        });
        return services;
    }

    public static WebApplication UseGrpcServices(this WebApplication app)
    {
        app.UseRouting();
        app.MapGrpcService<GrpcUploadFileService>();
        return app;
    }
}