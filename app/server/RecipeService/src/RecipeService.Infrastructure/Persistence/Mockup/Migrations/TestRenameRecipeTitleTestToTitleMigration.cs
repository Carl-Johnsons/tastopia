using MongoDB.Bson;
using MongoDB.Driver;
namespace RecipeService.Infrastructure.Persistence.Mockup.Migrations;

public class TestRenameRecipeTitleTestToTitleMigration : IMigration
{
    public string Version => "1.0.1";
    public async Task Up(IMongoDatabase database)
    {
        var collection = database.GetCollection<BsonDocument>("Recipe");

        var update = new BsonDocument("$rename", new BsonDocument
        {
            { "TitleTest", "Title" }
        });

        await collection.UpdateManyAsync(FilterDefinition<BsonDocument>.Empty, update);
    }

    public async Task Down(IMongoDatabase database)
    {
        var collection = database.GetCollection<BsonDocument>("Recipe");

        var update = new BsonDocument("$rename", new BsonDocument
        {
            { "Title", "TitleTest" }
        });

        await collection.UpdateManyAsync(FilterDefinition<BsonDocument>.Empty, update);
    }
}
