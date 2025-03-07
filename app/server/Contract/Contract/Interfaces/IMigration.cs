using MongoDB.Driver;
using System.Threading.Tasks;
namespace Contract.Interfaces;
public interface IMigration
{
    string Version { get; }
    Task Up(IMongoDatabase database);
    Task Down(IMongoDatabase database);
}
