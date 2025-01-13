using Contract.Utilities;
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
        await SeedRecipeTags();
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
                //add author
                int randomIndex = random.Next(RecipeData.Authors.Count);
                recipe.AuthorId = RecipeData.Authors[randomIndex];

                //add id for step
                foreach(var step in recipe.Steps)
                {
                    step.Id = Guid.NewGuid();
                }

                //add comment
                int numberOfComment = random.Next(35);
                for(int i = 0; i <= numberOfComment; i++)
                {
                    var randomIndexAccount = GetRandomExcluding(RecipeData.Authors.Count, randomIndex);
                    var comment = CommentData.GetRandomComment();
                    comment.AccountId = RecipeData.Authors[randomIndexAccount];
                    recipe.Comments.Add(comment);
                }
                recipe.NumberOfComment = recipe.Comments.Count;

                //add vote
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
            foreach (var tag in TagData.Data)
            {
                _context.Tags.Add(tag);
            }

            await _unitOfWork.SaveChangeAsync();
            Console.WriteLine("===================================================================================");
        }
    }

    public async Task SeedRecipeTags()
    {
        if (!_context.RecipeTags.Any())
        {
            Console.WriteLine("Begin seed recipe tag");
            Console.WriteLine("===================================================================================");
            foreach (var recipeTag in RecipeTagData.Data)
            {
                _context.RecipeTags.Add(recipeTag);
            }
            await _unitOfWork.SaveChangeAsync();
            Console.WriteLine("===================================================================================");
        }
    }

    public static int GetRandomExcluding(int range, int exclude)
    {
        Random random = new Random();
        int result;
        do
        {
            result = random.Next(range);
        } while (result == exclude);

        return result;
    }

}
