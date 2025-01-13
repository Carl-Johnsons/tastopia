namespace UserService.Domain.Interfaces;

public interface IConsulRegistryService
{
    Uri? GetServiceUri(string serviceName);
}