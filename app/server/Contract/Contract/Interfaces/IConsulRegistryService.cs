namespace Contract.Interfaces;

public interface IConsulRegistryService
{
    Uri? GetServiceUri(string serviceName);
    Uri? GetGrpcServiceUri(string serviceName);
}