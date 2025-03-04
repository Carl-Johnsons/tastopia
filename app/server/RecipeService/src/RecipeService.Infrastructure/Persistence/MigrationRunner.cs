using System.Reflection;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RecipeService.Infrastructure.Persistence;
public class MigrationRunner
{
    private readonly IMongoDatabase _database;
    private readonly List<IMigration> _migrations;

    public MigrationRunner(IMongoDatabase database)
    {
        _database = database;
        _migrations = LoadMigrations();
    }

    private List<IMigration> LoadMigrations()
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IMigration).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(t => (IMigration)Activator.CreateInstance(t)!)
            .OrderBy(m => m.Version)
            .ToList();
    }

    public async Task ApplyMigrationsAsync()
    {
        var migrationCollection = _database.GetCollection<BsonDocument>("Migrations");
        var appliedMigrations = (await migrationCollection.Find(_ => true).ToListAsync())
            .Select(m => m["Version"].AsString)
            .ToHashSet();

        foreach (var migration in _migrations)
        {
            if (!appliedMigrations.Contains(migration.Version))
            {
                await migration.Up(_database);
                await migrationCollection.InsertOneAsync(new BsonDocument { { "Version", migration.Version } });
            }
        }
    }

    public async Task RollbackMigrationsAsync()
    {
        var migrationCollection = _database.GetCollection<BsonDocument>("Migrations");

        foreach (var migration in _migrations.OrderByDescending(m => m.Version))
        {
            await migration.Down(_database);
            await migrationCollection.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("Version", migration.Version));
        }
    }
}

