using Consul;
using RecipeWorker.Interfaces;

namespace RecipeWorker.Services;

public class ConsulRegistryService : IConsulRegistryService
{
    private IConsulClient _consulClient;
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
        _logger.LogInformation($"https://{services[0].Service.Address}:{services[0].Service.Port}");
        return new Uri($"https://{services[0].Service.Address}:{services[0].Service.Port}");
    }
}
