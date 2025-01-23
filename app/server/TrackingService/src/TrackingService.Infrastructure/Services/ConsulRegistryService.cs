using Consul;
using Microsoft.Extensions.Logging;

namespace TrackingService.Infrastructure.Services;

public class ConsulRegistryService : IConsulRegistryService
{
    private readonly IConsulClient _consulClient;
    private readonly ILogger<ConsulRegistryService> _logger;

    public ConsulRegistryService(IConsulClient consulClient, ILogger<ConsulRegistryService> logger)
    {
        _consulClient = consulClient;
        _logger = logger;
    }

    public Uri? GetServiceUri(string serviceName)
    {
        var serviceQueryResult = _consulClient.Health.Service(serviceName).Result;

        if (serviceQueryResult == null
            || serviceQueryResult.Response == null
            || serviceQueryResult.Response.Length == 0)
        {
            return null;
        }

        var services = serviceQueryResult.Response;
        var uri = $"https://{services[0].Service.Address}:{services[0].Service.Port}";

        _logger.LogInformation($"Service queried successfully with: {uri}");
        return new Uri(uri);
    }
}
