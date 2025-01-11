namespace RecipeService.Domain.Interfaces;

public interface IConsulRegistryService
{
    Uri? GetServiceUri(string serviceName);
}