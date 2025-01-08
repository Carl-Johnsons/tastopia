using MongoDB.Driver;

namespace RecipeService.Domain.Interfaces;
public interface IDbContext : IDisposable
{
    DbContext Instance { get; }
    public IMongoDatabase GetDatabase();

}
