namespace RecipeWorker.Interfaces;

public interface IConsulRegistryService
{
    Uri? GetServiceUri(string serviceName);
}