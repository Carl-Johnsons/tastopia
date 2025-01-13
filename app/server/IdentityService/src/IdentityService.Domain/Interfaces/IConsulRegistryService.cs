namespace IdentityService.Domain.Interfaces;

public interface IConsulRegistryService
{
    Uri? GetServiceUri(string serviceName);
}