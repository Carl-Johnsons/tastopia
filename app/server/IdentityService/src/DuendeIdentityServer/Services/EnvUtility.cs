namespace DuendeIdentityServer.Services;

public class EnvUtility
{
    public static void LoadEnvFile()
    {
        var rootEnvPath = FindRootEnvFile();
        if (string.IsNullOrEmpty(rootEnvPath))
        {
            return;
        }

        DotNetEnv.Env.Load(rootEnvPath);


        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
        {
            DotNetEnv.Env.Load(".env.production");
        }
        else
        {
            DotNetEnv.Env.Load(".env");
        }
    }

    private static string? FindRootEnvFile()
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (currentDirectory != null && currentDirectory.Exists)
        {
            if (currentDirectory.Name.Equals("tastopia", StringComparison.OrdinalIgnoreCase))
            {
                return Path.Combine(currentDirectory.FullName, ".env");
            }
            currentDirectory = currentDirectory.Parent;
        }

        return null;
    }

}
