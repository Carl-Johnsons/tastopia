using MongoDB.Driver;
namespace RecipeService.Domain.Interfaces;
public interface IMigration
{
    string Version { get; }
    Task Up(IMongoDatabase database);
    Task Down(IMongoDatabase database);
}
