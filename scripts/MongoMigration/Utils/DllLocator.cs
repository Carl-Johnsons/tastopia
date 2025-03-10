namespace MongoMigration.Utils;

internal class DllLocator
{
    // Given the full path to a migration .cs file,
    // search upward for the .csproj file.
    public static string? FindProjectFile(string migrationFilePath)
    {
        var directory = new DirectoryInfo(Path.GetDirectoryName(migrationFilePath));
        while (directory != null)
        {
            var csproj = directory.GetFiles("*.csproj").FirstOrDefault();
            if (csproj != null)
                return csproj.FullName;
            directory = directory.Parent;
        }
        return null;
    }

    // Based on the .csproj location, construct an expected output DLL path.
    // This example assumes a Debug build and that the target framework is known (e.g., net8.0).
    public static string? GetOutputDllPath(string csprojPath, string targetFramework = "net8.0", string configuration = "Debug")
    {
        var projectDirectory = Path.GetDirectoryName(csprojPath);
        // Assume the assembly name is the same as the csproj file name (without extension)
        var projectName = Path.GetFileNameWithoutExtension(csprojPath);
        // Construct the path: <projectDir>/bin/<Configuration>/<targetFramework>/<ProjectName>.dll
        var dllPath = Path.Combine(projectDirectory, "bin", configuration, targetFramework, $"{projectName}.dll");
        return File.Exists(dllPath) ? dllPath : null;
    }

    // Main method to locate the DLL given a migration file path.
    public static string? LocateDllFromMigrationFile(string migrationFilePath)
    {
        var csprojPath = FindProjectFile(migrationFilePath);
        if (csprojPath == null)
        {
            Console.WriteLine("Could not find a .csproj file in the parent directories.");
            return null;
        }
        // You might allow passing configuration and target framework as options.
        var dllPath = GetOutputDllPath(csprojPath);
        if (dllPath == null)
        {
            Console.WriteLine("Could not locate the DLL. Make sure the project is built.");
            return null;
        }
        return dllPath;
    }
}
