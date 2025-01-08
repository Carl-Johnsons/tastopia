using Contract.Utilities;
using MongoDB.Driver;

namespace TrackingService.Infrastructure.Persistence.Mockup;

internal class MockupData
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public MockupData(ApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedAllData()
    {
        await EnsureDatabaseIsReady();
    }
    private async Task EnsureDatabaseIsReady()
    {
        EnvUtility.LoadEnvFile();
        var db = DotNetEnv.Env.GetString("DB", "Not found").Trim();
        var client = new MongoClient($"{EnvUtility.GetMongoDBWithoutAdminConnectionString()}/{db}?authSource=admin");  // Create MongoDB client

        var databaseList = await client.ListDatabasesAsync();
        var databases = await databaseList.ToListAsync();

        Console.WriteLine("====================================================");
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(databases));

        if (!databases.Any(d => d["name"] == db))
        {
            Console.WriteLine($"Database '{db}' does not exist. It will be created when data is inserted.");
        }
    }
}
