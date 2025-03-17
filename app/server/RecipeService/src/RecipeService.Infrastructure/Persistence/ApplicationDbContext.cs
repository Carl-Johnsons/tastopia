using Contract.Utilities;
using DnsClient.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ILogger<ApplicationDbContext> _logger;

    public ApplicationDbContext(ILogger<ApplicationDbContext> logger)
    {
        _logger = logger;
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
        : base(options)
    {
        _logger = logger;
    }


    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Domain.Entities.Tag> Tags { get; set; }

    //Relationship
    public DbSet<RecipeTag> RecipeTags { get; set; }

    public DbSet<UserBookmarkRecipe> UserBookmarkRecipes { get; set; }
    public DbSet<UserReportRecipe> UserReportRecipes { get; set; }
    public DbSet<UserReportComment> UserReportComments { get; set; }
    public DbSet<UserRecipeBin> UserRecipeBins { get; set; }

    public DbContext Instance => this;

    /**
     * <summary>
     * BsonSerializer use to make mongo driver understand guid
     * </summary>
     */
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        EnvUtility.LoadEnvFile();
        var db = DotNetEnv.Env.GetString("DB", "RecipeDB").Trim();
        var mongoConnectionString = EnvUtility.GetMongoDBConnectionString();

        _logger.LogInformation($"DB Connection string: {mongoConnectionString}");

        optionsBuilder.UseMongoDB(mongoConnectionString, db);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseAuditableEntity).IsAssignableFrom(entityType.ClrType))
            {
                var createdAtProperty = entityType.FindProperty("CreatedAt");
                var updatedAtProperty = entityType.FindProperty("UpdatedAt");

                if (createdAtProperty != null)
                {
                    createdAtProperty.SetValueConverter(
                        new ValueConverter<DateTime, DateTime>(
                            v => v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }

                if (updatedAtProperty != null)
                {
                    updatedAtProperty.SetValueConverter(
                        new ValueConverter<DateTime, DateTime>(
                            v => v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }
        }
        modelBuilder.Entity<Recipe>().ToCollection("Recipe"); ;
        modelBuilder.Entity<Domain.Entities.Tag>().ToCollection("Tag");
        modelBuilder.Entity<Domain.Entities.Tag>(entity =>
            {
                entity.Property(e => e.Status)
                      .HasConversion(typeof(string));
            });

        modelBuilder.Entity<UserReportComment>(entity =>
        {
            entity.Property(e => e.Status)
                  .HasConversion(typeof(string));
        });

        modelBuilder.Entity<UserReportRecipe>(entity =>
        {
            entity.Property(e => e.Status)
                  .HasConversion(typeof(string));
        });

        modelBuilder.Entity<RecipeTag>().ToCollection("RecipeTag");
    }

    /**
     * <summary>
     * This function used to get mongo database to get mongodb entity collection
     * </summary>
     */
    public IMongoDatabase GetDatabase()
    {
        EnvUtility.LoadEnvFile();

        var db = DotNetEnv.Env.GetString("DB", "RecipeDB").Trim();
        var mongoConnectionString = EnvUtility.GetMongoDBConnectionString();
        var client = new MongoClient(mongoConnectionString);
        return client.GetDatabase(db);
    }
}
