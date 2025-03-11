using Cocona;
using MongoMigration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = CoconaApp.CreateBuilder();

        var app = builder.Build();

        app.AddSubCommand("migrations", sub => {
            sub.AddCommands<MigrationCommands>();
        });

        app.AddSubCommand("database", sub => {
            sub.AddCommands<DatabaseCommands>();
        });

        app.Run();
    }
}