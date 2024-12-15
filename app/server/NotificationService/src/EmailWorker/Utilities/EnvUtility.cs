namespace EmailWorker.Utilities;

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
