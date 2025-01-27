using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Contract.Interfaces;
public interface IMongoDbContext : IDisposable
{
    DbContext Instance { get; }

    public IMongoDatabase GetDatabase();

}
