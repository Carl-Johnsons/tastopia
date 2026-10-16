using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Contract.Extension;

public static class OpenTelemetryExtension
{
    public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, string serviceName)
    {
        services.AddOpenTelemetry()
          .ConfigureResource(resource => resource
              .AddService(serviceName))
          .WithTracing(tracing => tracing
              .AddAspNetCoreInstrumentation(options =>
              {
                  options.Filter = httpContext =>
                  {
                      var path = httpContext.Request.Path.Value;
                      return path?.StartsWith("/health", StringComparison.OrdinalIgnoreCase) != true;
                  };
              })
              .AddHttpClientInstrumentation()
              .AddSource("MassTransit")
              .AddSource("Npgsql")
              .AddSource("MongoDB.Driver.Core.Extensions.DiagnosticSources")
              .AddOtlpExporter());
        return services;
    }
}
