namespace APIGateway.Interfaces;

public interface IConsulRegistryService
{
    Uri? GetServiceUri(string serviceName);
}