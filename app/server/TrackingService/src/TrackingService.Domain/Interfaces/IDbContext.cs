using MongoDB.Driver;

namespace TrackingService.Domain.Interfaces;
public interface IDbContext : IDisposable
{
    DbContext Instance { get; }

    public IMongoDatabase GetDatabase();

}
