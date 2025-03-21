using Contract.Constants;
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

            var wrongRecipeFile = File.ReadAllText(Path.Combine(SeedDataPath, "wrong-recipes.json"));
            var seedWrongRecipes = JsonConvert.DeserializeObject<List<SeedWrongRecipe>>(wrongRecipeFile) ?? [];

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


            foreach (var seedRecipe in seedWrongRecipes)
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
                // Add user report
                int numberOfReports = random.Next(5) + 1;
                var author = seedAccounts.SingleOrDefault(sa => sa.Id == recipe.AuthorId.ToString());
                List<SeedAccount> seedAccountWithoutAuthorId = JsonConvert.DeserializeObject<List<SeedAccount>>(JsonConvert.SerializeObject(seedAccounts))!;
                seedAccountWithoutAuthorId.Remove(author!);

                var randomIndexes = GetRandomIndexes(random, seedAccounts.Count, numberOfReports);

                foreach (var accountRandomIndex in randomIndexes)
                {
                    _context.UserReportRecipes.Add(new UserReportRecipe
                    {
                        AdditionalDetails = seedRecipe.AdditionalDetails,
                        ReasonCodes = seedRecipe.ReasonCodes,
                        EntityId = recipeId,
                        Status = ReportStatus.Pending,
                        AccountId = Guid.Parse(seedAccounts[accountRandomIndex].Id)
                    });
                }

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

            var seedPendingTagFile = File.ReadAllText(Path.Combine(SeedDataPath, "pending-tags.json"));
            var seedPendingTags = JsonConvert.DeserializeObject<List<SeedTag>>(seedPendingTagFile) ?? [];
            var pendingTags = new List<Tag>();
            foreach (var seedTag in seedPendingTags)
            {
                pendingTags.Add(new Tag
                {
                    Id = Guid.NewGuid(),
                    Code = "",
                    Value = seedTag.Value,
                    Category = Enum.Parse<TagCategory>(seedTag.Category),
                    ImageUrl = "",
                    Status = TagStatus.Pending
                });
            }

            _logger.LogInformation("Begin pending seed tag");
            _context.Tags.AddRange(pendingTags);

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

            var pendingTags = _context.Tags.Where(t => t.Status == TagStatus.Pending).ToList();
            foreach(var pendingTag in pendingTags)
            {
                var randomRecipe = await _context.Recipes
                .OrderBy(r => Guid.NewGuid())
                .FirstOrDefaultAsync();
                var rt = new RecipeTag
                {
                    TagId = pendingTag.Id,
                    RecipeId = randomRecipe!.Id
                };
                _context.RecipeTags.Add(rt);
            }
            await _unitOfWork.SaveChangeAsync();
        }
    }

    // Fisher-Yates Shuffle algorithm
    private List<int> GetRandomIndexes(Random random, int max, int count)
    {
        var indexes = Enumerable.Range(0, max).ToList();
        for (int i = max - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (indexes[i], indexes[j]) = (indexes[j], indexes[i]); // Swap
        }
        return indexes.Take(count).ToList();
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
    private class SeedWrongRecipe
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int? Serves { get; set; }
        public string? CookTime { get; set; }
        public List<string> Ingredients { get; set; } = null!;
        public List<SeedStep> Steps { get; set; } = [];
        public List<string> TagsCode { get; set; } = null!;
        public List<string> ReasonCodes { get; set; } = null!;
        public string AdditionalDetails { get; set; } = null!;
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

