using Contract.Utilities;
using MongoDB.Driver;
using TrackingService.Domain.Entities;
using TrackingService.Infrastructure.Persistence.Mockup.Data;

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
        await SeedUserViewRecipeDetails();
    }
    private async Task EnsureDatabaseIsReady()
    {
        EnvUtility.LoadEnvFile();
        var db = DotNetEnv.Env.GetString("DB", "Not found").Trim();
        var client = new MongoClient($"{EnvUtility.GetMongoDBWithoutAdminConnectionString()}/{db}?authSource=admin");  // Create MongoDB client

        var databaseList = await client.ListDatabasesAsync();
        var databases = await databaseList.ToListAsync();

        if (!databases.Any(d => d["name"] == db))
        {
            Console.WriteLine($"Database '{db}' does not exist. It will be created when data is inserted.");
        }
    }

    private async Task SeedUserViewRecipeDetails()
    {
        if (!_context.UserViewRecipeDetails.Any())
        {
            Console.WriteLine("Begin seed user view recipe detail");
            Console.WriteLine("===================================================================================");
            foreach(var accountId in UserViewRecipeDetailData.Accounts)
            {
                foreach(var recipeId in UserViewRecipeDetailData.Recipes)
                {
                    var time = UserViewRecipeDetailData.GetRandomDateTime();
                    var view = new UserViewRecipeDetail
                    {
                        Id = Guid.NewGuid(),
                        AccountId = accountId,
                        RecipeId = recipeId,
                        CreatedAt = time,
                        UpdatedAt = time,
                    };
                    _context.UserViewRecipeDetails.Add(view);
                }
            }
            _context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
            await _context.SaveChangesAsync();
            Console.WriteLine("===================================================================================");
        }
    }
}
