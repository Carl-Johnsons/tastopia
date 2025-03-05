using Cocona;

namespace MongoMigration;

internal class DatabaseCommands
{
    public DatabaseCommands()
    {
    }

    [Command("update")]
    public void Update([Argument] string migrationName)
    {
    }
}
