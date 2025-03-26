using Contract.Constants;
using Contract.Utilities;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Infrastructure.Persistence.Mockup.Data;
using static RecipeService.Infrastructure.Persistence.Mockup.Data.CommentData;
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
        if (_context.Recipes.Any())
            return null;

        var seedRecipeFile = File.ReadAllText(Path.Combine(SeedDataPath, "recipes.json"));
        var seedRecipes = JsonConvert.DeserializeObject<List<SeedRecipe>>(seedRecipeFile) ?? new List<SeedRecipe>();

        var wrongRecipeFile = File.ReadAllText(Path.Combine(SeedDataPath, "wrong-recipes.json"));
        var seedWrongRecipes = JsonConvert.DeserializeObject<List<SeedWrongRecipe>>(wrongRecipeFile) ?? new List<SeedWrongRecipe>();

        var seedAccountFile = File.ReadAllText(Path.Combine(SeedDataPath, "accounts.json"));
        var seedAccounts = JsonConvert.DeserializeObject<List<SeedAccount>>(seedAccountFile) ?? new List<SeedAccount>();
        seedAccounts = seedAccounts.Where(sa => sa.RoleCode == "USER").ToList();

        var mapRecipeTagsCode = new Dictionary<Guid, List<string>>();
        Random random = new Random();

        // Process normal recipes
        foreach (var seedRecipe in seedRecipes)
        {
            var recipe = CreateRecipe(seedRecipe, seedAccounts, random);
            _context.Recipes.Add(recipe);
            mapRecipeTagsCode.Add(recipe.Id, seedRecipe.TagsCode);
        }

        // Process wrong recipes and add extra user report logic
        foreach (var seedWrongRecipe in seedWrongRecipes)
        {
            var recipe = CreateRecipe(seedWrongRecipe, seedAccounts, random);
            _context.Recipes.Add(recipe);
            AddUserReports(recipe, seedWrongRecipe, seedAccounts, random);
            mapRecipeTagsCode.Add(recipe.Id, seedWrongRecipe.TagsCode);
        }

        await _unitOfWork.SaveChangeAsync();
        return mapRecipeTagsCode;
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

    private Recipe CreateRecipe(dynamic seedRecipe, List<SeedAccount> seedAccounts, Random random)
    {
        var steps = CreateSteps(seedRecipe.Steps);
        var recipeId = GenerateUniqueRecipeId();
        var time = GetRandomDateTime().ToUniversalTime();
        var recipe = new Recipe
        {
            Id = recipeId,
            Title = seedRecipe.Title,
            Description = seedRecipe.Description,
            ImageUrl = seedRecipe.ImageUrl,
            Serves = seedRecipe.Serves,
            CookTime = seedRecipe.CookTime,
            Ingredients = seedRecipe.Ingredients,
            Steps = steps,
            IsActive = true,
            CreatedAt = time,
            UpdatedAt = time,
            TotalView = GetRandomNumber(5, 1000),
        };

        // Set author, comments and votes
        int randomIndex = random.Next(seedAccounts.Count);
        recipe.AuthorId = Guid.Parse(seedAccounts[randomIndex].Id);

        AddComments(recipe, seedAccounts, random, randomIndex);
        AddVotes(recipe, seedAccounts, random);

        return recipe;
    }

    private Guid GenerateUniqueRecipeId()
    {
        Guid recipeId;
        do
        {
            recipeId = Guid.NewGuid();
        } while (_context.Recipes.Any(r => r.Id == recipeId));
        return recipeId;
    }

    private void AddComments(Recipe recipe, List<SeedAccount> seedAccounts, Random random, int currentAuthorIndex)
    {
        int numberOfComment = random.Next(35);

        int wrongCommentRate = 10;
        int wrongCommentChance;
        Guid commentId;

        for (int i = 0; i <= numberOfComment; i++)
        {
            var randomIndexAccount = GetRandomExcluding(seedAccounts.Count, currentAuthorIndex);
            wrongCommentChance = random.Next(100);
            Comment comment;

            do
            {
                commentId = Guid.NewGuid();
            } while (recipe.Comments.Any(c => c.Id == commentId));

            if (wrongCommentChance < wrongCommentRate)
            {
                var wrongComment = CommentData.GetRandomWrongCommentContent();

                AddUserReports(recipe, commentId, wrongComment, seedAccounts, random);
                var time = CommentData.GetRandomDateTime();
                comment = new Comment
                {
                    Id = commentId,
                    Content = wrongComment.Content,
                    CreatedAt = time,
                    UpdatedAt = time,
                    IsActive = true,
                };
            }
            else
            {
                comment = CommentData.GetRandomComment(commentId);
            }

            comment.AccountId = Guid.Parse(seedAccounts[randomIndexAccount].Id);
            recipe.Comments.Add(comment);

        }
        recipe.NumberOfComment = recipe.Comments.Count;
    }

    private void AddVotes(Recipe recipe, List<SeedAccount> seedAccounts, Random random)
    {
        var numberOfVote = random.Next(seedAccounts.Count);
        recipe.VoteDiff = numberOfVote;
        var recipeVotes = new List<RecipeVote>();

        for (int i = 0; i < numberOfVote; i++)
        {
            var accountId = Guid.Parse(seedAccounts[i].Id);
            Guid voteId = Guid.NewGuid();
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
    }

    private void AddUserReports(Recipe recipe, SeedWrongRecipe seedWrongRecipe, List<SeedAccount> seedAccounts, Random random)
    {
        int numberOfReports = random.Next(5) + 1;
        // Exclude the author from potential reporters
        var reportingAccounts = seedAccounts.Where(sa => sa.Id != recipe.AuthorId.ToString()).ToList();
        var randomIndexes = GetRandomIndexes(random, reportingAccounts.Count, numberOfReports);

        foreach (var idx in randomIndexes)
        {
            // Determine a random number of reason codes to select (at least 1, at most all)
            int numberOfCodesToSelect = random.Next(1, seedWrongRecipe.ReasonCodes.Count + 1);
            // Shuffle and take a subset
            var reasonCodesSubset = seedWrongRecipe.ReasonCodes
                .OrderBy(x => random.Next())
                .Take(numberOfCodesToSelect)
                .ToList();

            _context.UserReportRecipes.Add(new UserReportRecipe
            {
                AdditionalDetails = seedWrongRecipe.AdditionalDetails,
                ReasonCodes = reasonCodesSubset,
                EntityId = recipe.Id,
                Status = ReportStatus.Pending,
                AccountId = Guid.Parse(reportingAccounts[idx].Id)
            });
        }
    }

    private void AddUserReports(Recipe recipe, Guid commentId, SeedWrongComment seedWrongComment, List<SeedAccount> seedAccounts, Random random)
    {
        int numberOfReports = random.Next(5) + 1;
        // Exclude the author from potential reporters
        var reportingAccounts = seedAccounts.Where(sa => sa.Id != recipe.AuthorId.ToString()).ToList();
        var randomIndexes = GetRandomIndexes(random, reportingAccounts.Count, numberOfReports);

        foreach (var idx in randomIndexes)
        {
            var randomDetailIndex = random.Next(seedWrongComment.AdditionalDetails.Count);
            // Determine a random number of reason codes to select (at least 1, at most all)
            int numberOfCodesToSelect = random.Next(1, seedWrongComment.ReasonCodes.Count + 1);
            // Shuffle and take a subset
            var reasonCodesSubset = seedWrongComment.ReasonCodes
                .OrderBy(x => random.Next())
                .Take(numberOfCodesToSelect)
                .ToList();

            _context.UserReportComments.Add(new UserReportComment
            {
                AdditionalDetails = seedWrongComment.AdditionalDetails[randomDetailIndex],
                ReasonCodes = reasonCodesSubset,
                RecipeId = recipe.Id,
                EntityId = commentId,
                Status = ReportStatus.Pending,
                AccountId = Guid.Parse(reportingAccounts[idx].Id)
            });
        }
    }

    private List<Step> CreateSteps(List<SeedStep> seedSteps)
    {
        var steps = new List<Step>();
        for (int i = 0; i < seedSteps.Count; i++)
        {
            Guid stepId = Guid.NewGuid();
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
        return steps;
    }
    //random datetime from 1/1/2023 - 12/12/2024
    private DateTime GetRandomDateTime()
    {
        Random random = new Random();
        DateTime start = new DateTime(2023, 11, 1);
        DateTime end = new DateTime(2025, 3, 24);

        int range = (end - start).Days;
        return start.AddDays(random.Next(range + 1))
                    .AddHours(random.Next(0, 24))
                    .AddMinutes(random.Next(0, 60))
                    .AddSeconds(random.Next(0, 60));
    }

    private int GetRandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max + 1);
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

