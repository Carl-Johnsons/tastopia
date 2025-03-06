using Cocona;
using Sprache;

namespace MongoMigration;

internal class MigrationCommands
{
    public MigrationCommands()
    {
    }

    [Command("add")]
    public void Add([Argument] string migrationName, [Option] string? outputFolder)
    {
        if (outputFolder == null) {
            outputFolder = "Migrations";
        }

        string currentDirectory = Directory.GetCurrentDirectory();
        string folderPath = Path.Combine(currentDirectory, outputFolder);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Generate a timestamp in milliseconds.
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        string fileName = $"{timestamp}_{migrationName}.cs";
        string fullPath = Path.Combine(folderPath, fileName);
        string template = GetMigrationTemplate(migrationName);
        File.WriteAllText(fullPath, template);

        Console.WriteLine($"New migration file created: {fullPath}");
    }

    [Ignore]
    private string GetMigrationTemplate(string migrationName)
    {
        // This template assumes you have an interface IMigration with UpAsync/DownAsync methods.
        var normalizeMigrationName = char.ToUpper(migrationName[0]) + migrationName.Substring(1);
        return @$"
using MongoDB.Driver;
using Contract.Interfaces;

public class {normalizeMigrationName} : IMigration
{{
    // Specify the target version for this migration.
    public string Version => ""<specify version>"";

    public async Task Up(IMongoDatabase database)
    {{
        // Write code to apply changes (e.g. rename fields, update data, etc.)
    }}

    public async Task Down(IMongoDatabase database)
    {{
        // Write code to rollback changes if needed.
    }}
}}";
    }

}
