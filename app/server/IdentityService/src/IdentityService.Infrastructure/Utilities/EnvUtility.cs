namespace IdentityService.Infrastructure.Utilities;

public class EnvUtility
{
    public static void LoadEnvFile()
    {
        var rootEnvPath = GetEnvFilePath("tastopia", ".env");
        if (string.IsNullOrEmpty(rootEnvPath))
        {
            return;
        }

        DotNetEnv.Env.Load(rootEnvPath);

        string solutionPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName ?? "";


        if (DotNetEnv.Env.GetString("ASPNETCORE_ENVIRONMENT", "") == "Production")
        {
            DotNetEnv.Env.Load(Path.Combine(solutionPath, ".env.production"));
        }
        else
        {
            DotNetEnv.Env.Load(Path.Combine(solutionPath, ".env"));
        }
    }

    public static string GetConnectionString()
    {
        LoadEnvFile();
        var host = DotNetEnv.Env.GetString("HOST", "localhost").Trim();

        var port = DotNetEnv.Env.GetString("DB_PORT", "2001").Trim();
        var db = DotNetEnv.Env.GetString("DB", "Not found").Trim();
        var user = DotNetEnv.Env.GetString("POSTGRES_USER", "Not found").Trim();
        var pwd = DotNetEnv.Env.GetString("POSTGRES_PASSWORD", "Not found").Trim();
        var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={pwd};";
        Console.WriteLine(connectionString);
        return connectionString;
    }


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

}
