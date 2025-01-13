namespace Contract.Utilities;

public class EnvUtility
{
    public static void LoadEnvFile()
    {
        var rootEnvPath = GetEnvFilePath("tastopia", ".env");
        if (string.IsNullOrEmpty(rootEnvPath))
        {
            return;
        }

        string solutionPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName ?? "";

        if (IsProduction())
        {
            DotNetEnv.Env.Load(rootEnvPath);
            DotNetEnv.Env.Load(Path.Combine(solutionPath, ".env.production"));
        }
        else if (IsDevelopment())
        {
            LoadEnvWithoutOverriding(rootEnvPath);
            LoadEnvWithoutOverriding(Path.Combine(solutionPath, ".env"));
        }
    }
    public static bool IsDevelopment()
    {
        return DotNetEnv.Env.GetString("ASPNETCORE_ENVIRONMENT", "") == "Development";
    }
    public static bool IsProduction()
    {
        return DotNetEnv.Env.GetString("ASPNETCORE_ENVIRONMENT", "") == "Production";
    }

    /// <summary>
    /// Get the postgresql connection string from env
    /// </summary>
    /// <returns></returns>
    public static string GetConnectionString()
    {
        LoadEnvFile();
        var host = DotNetEnv.Env.GetString("DB_HOST", "localhost").Trim();

        var port = DotNetEnv.Env.GetString("DB_PORT", "2001").Trim();
        var db = DotNetEnv.Env.GetString("DB", "Not found").Trim();
        var user = DotNetEnv.Env.GetString("POSTGRES_USER", "Not found").Trim();
        var pwd = DotNetEnv.Env.GetString("POSTGRES_PASSWORD", "Not found").Trim();
        var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={pwd};";
        Console.WriteLine(connectionString);
        return connectionString;
    }

    /// <summary>
    ///  Get mongodb connection string without database name
    /// </summary>
    /// <returns></returns>
    public static string GetMongoDBConnectionString()
    {
        LoadEnvFile();
        var host = DotNetEnv.Env.GetString("MONGODB_HOST", "localhost").Trim();

        var port = DotNetEnv.Env.GetString("MONGODB_PORT", "2001").Trim();
        var user = DotNetEnv.Env.GetString("MONGO_INITDB_ROOT_USERNAME", "Not found").Trim();
        var pwd = DotNetEnv.Env.GetString("MONGO_INITDB_ROOT_PASSWORD", "Not found").Trim();
        var connectionString = $"mongodb://{user}:{pwd}@{host}:{port}?authSource=admin";
        Console.WriteLine(connectionString);
        return connectionString;
    }

    /// <summary>
    ///  Get mongodb connection string without database name and admin
    /// </summary>
    /// <returns></returns>
    public static string GetMongoDBWithoutAdminConnectionString()
    {
        LoadEnvFile();
        var host = DotNetEnv.Env.GetString("MONGODB_HOST", "localhost").Trim();

        var port = DotNetEnv.Env.GetString("MONGODB_PORT", "2001").Trim();
        var user = DotNetEnv.Env.GetString("MONGO_INITDB_ROOT_USERNAME", "Not found").Trim();
        var pwd = DotNetEnv.Env.GetString("MONGO_INITDB_ROOT_PASSWORD", "Not found").Trim();
        var connectionString = $"mongodb://{user}:{pwd}@{host}:{port}";
        Console.WriteLine(connectionString);
        return connectionString;
    }

    /// <summary>
    ///     Recursively search for the .env file by traversing up to the target parent directory.
    /// </summary>
    /// <param name="folderName"></param>
    /// <param name="envFileName"></param>
    /// <returns>absolute env file path, if not found return null</returns>
    private static string? GetEnvFilePath(string folderName, string envFileName)
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (currentDirectory != null && currentDirectory.Exists)
        {
            if (currentDirectory.Name.Equals(folderName, StringComparison.OrdinalIgnoreCase))
            {
                return Path.Combine(currentDirectory.FullName, envFileName);
            }
            currentDirectory = currentDirectory.Parent;
        }

        return null;
    }


    /// <summary>
    ///     Intended for use in the development environment only.
    /// </summary>
    /// <remarks>
    /// WARNING: This method is designed for running all service scripts during development. 
    /// Using it in production may lead to unexpected behavior.
    /// </remarks>

    private static void LoadEnvWithoutOverriding(string envPath)
    {
        if (!File.Exists(envPath))
        {
            Console.WriteLine("The .env file does not exist at the specified path.");
            return;
        }

        var envVariables = ParseEnvFile(envPath);

        foreach (var (key, value) in envVariables)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key)))
            {
                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }
    
    private static Dictionary<string, string> ParseEnvFile(string envPath)
    {
        var envVariables = new Dictionary<string, string>();

        foreach (var line in File.ReadLines(envPath))
        {
            // Skip comments and empty lines
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue;

            var parts = line.Split(new[] { '=' }, 2);
            if (parts.Length == 2)
            {
                var key = parts[0].Trim();
                var value = parts[1].Trim();

                if (!envVariables.ContainsKey(key))
                {
                    envVariables.Add(key, value);
                }
            }
        }

        return envVariables;
    }


}
