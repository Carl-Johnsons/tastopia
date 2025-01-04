using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Infrastructure.Persistence.Mockup.Data;
using RecipeService.Infrastructure.Utilities;

namespace RecipeService.Infrastructure.Persistence.Mockup;

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
        await SeedRecipes();
        await SeedTags();
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

    public async Task SeedRecipes()
    {
        if (!_context.Recipes.Any())
        {
            Console.WriteLine("Begin seed recipe");
            Console.WriteLine("===================================================================================");
            Random random = new Random();

            foreach (var recipe in RecipeData.Recipes)
            {
                int randomIndex = random.Next(RecipeData.Authors.Count);
                recipe.AuthorId = RecipeData.Authors[randomIndex];

                foreach(var step in recipe.Steps)
                {
                    step.Id = Guid.NewGuid();
                }

                foreach(var comment in recipe.Comments)
                {
                    comment.Id = Guid.NewGuid();
                }
                recipe.NumberOfComment = recipe.Comments.Count;

                var numberOfVote = random.Next(RecipeData.Authors.Count);
                recipe.VoteDiff = numberOfVote;
                for (int i = 0; i < numberOfVote; i++)
                {
                    var accountId = RecipeData.Authors[i];
                    recipe.RecipeVotes.Add(new RecipeVote
                    {
                        AccountId = accountId,
                        IsUpvote = true,
                        Id = Guid.NewGuid()
                    });
                }
                _context.Recipes.Add(recipe);
            }

            await _unitOfWork.SaveChangeAsync();
            Console.WriteLine("===================================================================================");
        }
    }

    public async Task SeedTags()
    {
        if (!_context.Tags.Any())
        {
            Console.WriteLine("Begin seed tag");
            Console.WriteLine("===================================================================================");
            Random random = new Random();

            foreach (var tag in TagData.Data)
            {
                _context.Tags.Add(tag);
            }

            await _unitOfWork.SaveChangeAsync();
            Console.WriteLine("===================================================================================");
        }
    }

}
