using Contract.Utilities;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Infrastructure.Persistence.Mockup.Data;
using Tag = RecipeService.Domain.Entities.Tag;

namespace RecipeService.Infrastructure.Persistence.Mockup;

internal class MockupData
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MockupData> _logger;
    private readonly string SeedDataPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName!, "seeds") ?? "";
    public MockupData(ApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<MockupData> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task SeedAllData()
    {
        await EnsureDatabaseIsReady();
        await SeedRecipeTags();
    }
    private async Task EnsureDatabaseIsReady()
    {
        _logger.LogInformation(SeedDataPath);
        EnvUtility.LoadEnvFile();
        var db = DotNetEnv.Env.GetString("DB", "Not found").Trim();
        var client = new MongoClient($"{EnvUtility.GetMongoDBWithoutAdminConnectionString()}/{db}?authSource=admin");  // Create MongoDB client

        var databaseList = await client.ListDatabasesAsync();
        var databases = await databaseList.ToListAsync();

        if (!databases.Any(d => d["name"] == db))
        {
            _logger.LogInformation($"Database '{db}' does not exist. It will be created when data is inserted.");
        }
    }

    public async Task<Dictionary<Guid, List<string>>?> SeedRecipes()
    {
        if (!_context.Recipes.Any())
        {
            var seedRecipeFile = File.ReadAllText(Path.Combine(SeedDataPath, "recipes.json"));
            var seedRecipes = JsonConvert.DeserializeObject<List<SeedRecipe>>(seedRecipeFile) ?? [];

            var seedAccountFile = File.ReadAllText(Path.Combine(SeedDataPath, "accounts.json"));
            var seedAccounts = JsonConvert.DeserializeObject<List<SeedAccount>>(seedAccountFile) ?? [];

            var seedCommentFile = File.ReadAllText(Path.Combine(SeedDataPath, "comments.json"));

            var mapRecipeTagsCode = new Dictionary<Guid, List<string>>();
            var recipes = new List<Recipe>();

            // Only role user for comment and vote
            seedAccounts = seedAccounts.Where(sa => sa.RoleCode == "USER").ToList();

            _logger.LogInformation("Begin seed recipe");
            Random random = new Random();

            foreach (var seedRecipe in seedRecipes)
            {
                var steps = new List<Step>();
                var seedSteps = seedRecipe.Steps;
                for (int i = 0; i < seedSteps.Count; i++)
                {
                    Guid stepId;
                    do
                    {
                        stepId = Guid.NewGuid();
                    } while (steps.Any(s => s.Id == stepId));

                    steps.Add(new Step
                    {
                        Id = stepId,
                        OrdinalNumber = i + 1,
                        Content = seedSteps[i].Content,
                        AttachedImageUrls = seedSteps[i].AttachedImageURLs,
                    });
                }

                Guid recipeId;

                do
                {
                    recipeId = Guid.NewGuid();
                } while (_context.Recipes.Any(r => r.Id == recipeId));

                var recipe = new Recipe
                {
                    Id = recipeId,
                    Title = seedRecipe.Title,
                    Description = seedRecipe.Description,
                    ImageUrl = seedRecipe.ImageUrl,
                    Serves = seedRecipe.Serves,
                    CookTime = seedRecipe.CookTime,
                    Ingredients = seedRecipe.Ingredients,
                    Steps = steps
                };
                //add author
                int randomIndex = random.Next(seedAccounts.Count);
                recipe.AuthorId = Guid.Parse(seedAccounts[randomIndex].Id);
                recipe.IsActive = true;
                recipe.CreatedAt = DateTime.UtcNow;
                recipe.UpdatedAt = DateTime.UtcNow;

                //add comment
                int numberOfComment = random.Next(35);
                for (int i = 0; i <= numberOfComment; i++)
                {
                    var randomIndexAccount = GetRandomExcluding(seedAccounts.Count, randomIndex);
                    var comment = CommentData.GetRandomComment();
                    comment.AccountId = Guid.Parse(seedAccounts[randomIndexAccount].Id);
                    recipe.Comments.Add(comment);
                }
                recipe.NumberOfComment = recipe.Comments.Count;

                //add vote
                var numberOfVote = random.Next(seedAccounts.Count);
                recipe.VoteDiff = numberOfVote;

                var recipeVotes = new List<RecipeVote>();
                Guid voteId;
                for (int i = 0; i < numberOfVote; i++)
                {
                    var accountId = Guid.Parse(seedAccounts[i].Id);
                    do
                    {
                        voteId = Guid.NewGuid();
                    } while (recipeVotes.Any(rv => rv.Id == voteId));

                    recipeVotes.Add(new RecipeVote
                    {
                        Id = voteId,
                        AccountId = accountId,
                        IsUpvote = true,
                    });
                }

                recipe.RecipeVotes.AddRange(recipeVotes);

                _context.Recipes.Add(recipe);

                mapRecipeTagsCode.Add(recipeId, seedRecipe.TagsCode);
            }
            await _unitOfWork.SaveChangeAsync();
            return mapRecipeTagsCode;
        }
        return null;
    }

    public async Task<Dictionary<string, Guid>?> SeedTags()
    {
        if (!_context.Tags.Any())
        {
            var seedTagFile = File.ReadAllText(Path.Combine(SeedDataPath, "tags.json"));
            var seedTags = JsonConvert.DeserializeObject<List<SeedTag>>(seedTagFile) ?? [];
            var tags = new List<Tag>();
            foreach (var seedTag in seedTags)
            {
                tags.Add(new Tag
                {
                    Code = seedTag.Code,
                    Value = seedTag.Value,
                    Category = Enum.Parse<TagCategory>(seedTag.Category),
                    ImageUrl = seedTag.ImageUrl,
                    Status = TagStatus.Active
                });
            }

            _logger.LogInformation("Begin seed tag");
            _context.Tags.AddRange(tags);

            await _unitOfWork.SaveChangeAsync();
            var mapTagCode = tags.ToDictionary(t => t.Code, t => t.Id);
            return mapTagCode;
        }
        return null;
    }

    public async Task SeedRecipeTags()
    {
        if (!_context.RecipeTags.Any())
        {
            var mapTagsCode = await SeedTags();
            var mapRecipeTagsCode = await SeedRecipes();
            if (mapTagsCode == null || mapRecipeTagsCode == null)
            {
                _logger.LogError("Please delete recipes and tags collection in order to seed data");
                return;
            }


            _logger.LogInformation("Begin seed recipe tag");
            foreach (var (recipeId, tagsCode) in mapRecipeTagsCode)
            {
                foreach (var tagCode in tagsCode)
                {
                    if (mapTagsCode.TryGetValue(tagCode, out Guid tagId))
                    {
                        _context.RecipeTags.Add(new RecipeTag
                        {
                            RecipeId = recipeId,
                            TagId = tagId
                        });
                    }
                    else
                    {
                        _logger.LogError($"There are no {tagCode} found in tags.json");
                    }
                }
            }
            await _unitOfWork.SaveChangeAsync();
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
    private class SeedRecipe
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int? Serves { get; set; }
        public string? CookTime { get; set; }
        public List<string> Ingredients { get; set; } = null!;
        public List<SeedStep> Steps { get; set; } = [];
        public List<string> TagsCode { get; set; } = null!;
    }

    private class SeedStep
    {
        public string Content { get; set; } = null!;

        public List<string> AttachedImageURLs { get; set; } = [];
    }

    private class SeedTag
    {
        public string Value { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
    private class SeedAccount
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string RoleCode { get; set; } = null!;
    }
}

