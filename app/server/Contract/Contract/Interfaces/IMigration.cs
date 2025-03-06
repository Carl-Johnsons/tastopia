using MongoDB.Driver;
namespace Contract.Interfaces;
public interface IMigration
{
    string Version { get; }
    Task Up(IMongoDatabase database);
    Task Down(IMongoDatabase database);
}
