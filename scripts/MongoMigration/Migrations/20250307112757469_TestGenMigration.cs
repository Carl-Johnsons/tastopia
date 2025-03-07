using MongoDB.Driver;
using Contract.Interfaces;
using MongoDB.Bson;
using System.Threading.Tasks;

public class TestGenMigration : IMigration
{
    // Specify the target version for this migration.
    public string Version => "<specify version>";

    public async Task Up(IMongoDatabase database)
    {
        var collection = database.GetCollection<BsonDocument>("Recipe");

        var update = new BsonDocument("$rename", new BsonDocument
        {
            { "Title", "TitleTest" }
        });
        await collection.UpdateManyAsync(FilterDefinition<BsonDocument>.Empty, update);
    }

    public async Task Down(IMongoDatabase database)
    {
        var collection = database.GetCollection<BsonDocument>("Recipe");

        var update = new BsonDocument("$rename", new BsonDocument
        {
            { "TitleTest", "Title" }
        });
        await collection.UpdateManyAsync(FilterDefinition<BsonDocument>.Empty, update);
    }
}