using Cocona;
using MongoDB.Driver;
using Contract.Utilities;
using System.Text.Json;
using MongoDB.Bson;
using Contract.Interfaces;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using MongoMigration.Utils;


namespace MongoMigration;

internal class DatabaseCommands
{
    private readonly HashSet<string> ExcludeDefaultDatabases = ["admin", "config", "local"];
    public DatabaseCommands()
    {
    }

    [Command("update")]
    public async Task Update([Argument] string? db)
    {
        if (db != null && ExcludeDefaultDatabases.Contains(db))
        {
            Console.WriteLine("Can't update default database");
            return;
        }

        var client = GetMongoClient();
        var databases = await client.ListDatabasesAsync();
        var dbNames = (await databases.ToListAsync())
                    .Where(db => !ExcludeDefaultDatabases.Contains(db["name"].AsString))
                    .Select(db => db["name"].AsString)
                    .ToList();
        var selectDbIndex = -1;

        if (db == null)
        {
            while (selectDbIndex == -1)
            {
                Console.WriteLine("\n\n====================================");
                Console.WriteLine("Choose a database in order to update");
                for (int i = 0; i < dbNames.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {dbNames[i]}");

                }
                if (int.TryParse(Console.ReadLine(), out int pos))
                {
                    if (pos > dbNames.Count || pos <= 0)
                    {
                        Console.WriteLine($"Invalid input! Please enter a number that in range [1-{dbNames.Count}]");
                    }
                    else
                    {
                        selectDbIndex = pos - 1;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                }
            }
        }
        else
        {
            var index = dbNames.IndexOf(db);
            if (index == -1)
            {
                Console.WriteLine("Database not exist! Aborting update ...");
                return;
            }
            selectDbIndex = index;
        }

        var database = client.GetDatabase(dbNames[selectDbIndex]);

        // Get the migrations then apply it
        string migrationFolder = Path.Combine(Directory.GetCurrentDirectory(), "Migrations");
        var migrationFiles = Directory.GetFiles(migrationFolder, "*.cs");

        var orderedFiles = migrationFiles.OrderBy(file =>
        {
            // Extract the prefix (assumes the first 17 characters are the timestamp: yyyyMMddHHmmssfff).
            string fileName = Path.GetFileNameWithoutExtension(file);
            string timestampStr = fileName.Substring(0, 17);
            DateTime.TryParseExact(timestampStr, "yyyyMMddHHmmssfff",
                                     null, System.Globalization.DateTimeStyles.None, out var ts);
            return ts;
        });

        var migrationCollection = database.GetCollection<BsonDocument>("Migrations");
        var appliedMigrations = (await migrationCollection.Find(_ => true).ToListAsync())
            .Select(m => m["MigrationId"].AsString)
            .ToHashSet();

        foreach (var file in orderedFiles)
        {
            // Use the file name (without extension) as the migration id.
            string migrationId = Path.GetFileNameWithoutExtension(file);

            if (!appliedMigrations.Contains(migrationId))
            {
                Console.WriteLine(Path.Combine(migrationFolder, file));

                var migration = LoadMigration(Path.Combine(migrationFolder, file));
                if (migration != null)
                {
                    await migration.Up(database);
                    var migrationRecord = new BsonDocument { { "MigrationId", migrationId }, { "CreatedAt", DateTime.UtcNow } };
                    await migrationCollection.InsertOneAsync(migrationRecord);
                    Console.WriteLine($"Applied {migrationId}");
                }
            }
        }

        Console.WriteLine(JsonSerializer.Serialize(dbNames));
    }

    [Ignore]
    private MongoClient GetMongoClient()
    {
        EnvUtility.LoadEnvFile();

        var mongoConnectionString = EnvUtility.GetMongoDBConnectionString();
        var mongostring = "mongodb://mongodb:15c0996dc7355e47ff0588570f2312296ebbe64e8e7beda986d1332f77785d39@localhost:2002?authSource=admin";
        var client = new MongoClient(mongostring);
        return client;
    }

    [Ignore]
    public static IMigration? LoadMigration(string filePath)
    {
        // Read the source code from the file.
        string code = File.ReadAllText(filePath);

        // Parse the syntax tree.
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        string? dllPath = DllLocator.LocateDllFromMigrationFile(filePath);
        if (dllPath == null)
        {
            Console.WriteLine("DLL not found. Please build the project containing the migration files first.");
            return null;
        }

        Assembly migrationAssembly = Assembly.LoadFrom(dllPath);
        var mainAssembly = Assembly.GetExecutingAssembly();
        var references = new List<MetadataReference>();
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (!string.IsNullOrEmpty(assembly.Location))
            {
                references.Add(MetadataReference.CreateFromFile(assembly.Location));
            }
        }
        string runtimeDir = Path.GetDirectoryName(typeof(object).Assembly.Location) ?? "";
        string[] coreLibs = new[]
        {
            "System.Private.CoreLib.dll",
            "System.Runtime.dll",
            "System.Console.dll",
            "System.Linq.dll",
            "System.Collections.dll",
            "System.Threading.Tasks.dll",
            "netstandard.dll"
        };
        foreach (var lib in coreLibs)
        {
            string libPath = Path.Combine(runtimeDir, lib);
            if (File.Exists(libPath))
            {
                references.Add(MetadataReference.CreateFromFile(libPath));
            }
        }

        var referencedAssemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();

        foreach (var asmName in referencedAssemblyNames)
        {
            try
            {
                // Load the assembly (if it's not already loaded).
                var loadedAssembly = Assembly.Load(asmName);
                if (!string.IsNullOrEmpty(loadedAssembly.Location))
                {
                    references.Add(MetadataReference.CreateFromFile(loadedAssembly.Location));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load assembly '{asmName.FullName}': {ex.Message}");
            }
        }


        // Create the compilation.
        var compilation = CSharpCompilation.Create(
            "DynamicMigrationAssembly",
            [syntaxTree],
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        // Emit the assembly to a memory stream.
        using (var ms = new MemoryStream())
        {
            var emitResult = compilation.Emit(ms);
            if (!emitResult.Success)
            {
                // Handle compilation errors.
                var errors = emitResult.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error);
                throw new Exception("Compilation failed: " + string.Join(Environment.NewLine, errors));
            }

            ms.Seek(0, SeekOrigin.Begin);
            var assembly = Assembly.Load(ms.ToArray());

            // Find a type that implements IMigration.
            var migrationType = assembly.GetTypes()
                .FirstOrDefault(t => typeof(IMigration).IsAssignableFrom(t) && !t.IsAbstract);

            if (migrationType == null)
            {
                throw new Exception("No migration type found in the compiled assembly.");
            }

            // Create an instance of the migration.
            return (IMigration?)Activator.CreateInstance(migrationType);
        }
    }
}
