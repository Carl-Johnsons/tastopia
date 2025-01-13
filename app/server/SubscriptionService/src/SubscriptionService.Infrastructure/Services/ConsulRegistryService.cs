using Consul;

namespace SubscriptionService.Infrastructure.Services;

public class ConsulRegistryService : IConsulRegistryService
{
    private IConsulClient _consulClient;

    public ConsulRegistryService(IConsulClient consulClient)
    {
        _consulClient = consulClient;
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
        Console.WriteLine($"https://{services[0].Service.Address}:{services[0].Service.Port}");
        return new Uri($"https://{services[0].Service.Address}:{services[0].Service.Port}");
    }
}
