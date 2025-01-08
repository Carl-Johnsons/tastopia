using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using RecipeService.Domain.Entities;
using RecipeService.Infrastructure.Utilities;

namespace RecipeService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Domain.Entities.Tag> Tags { get; set; }

    //Relationship
    public DbSet<RecipeTag> RecipeTags { get; set; }

    public DbSet<UserBookmarkRecipe> UserBookmarkRecipes { get; set; }
    public DbSet<UserReportRecipe> UserReportRecipes { get; set; }
    public DbSet<UserReportComment> UserReportComments { get; set; }

    public DbContext Instance => this;
    public ApplicationDbContext()
    {
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }
    /**
     * <summary>
     * BsonSerializer use to make mongo driver understand guid
     * </summary>
     */
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        EnvUtility.LoadEnvFile();
        var db = DotNetEnv.Env.GetString("DB", "RecipeDB").Trim();
        var mongoConnectionString = EnvUtility.GetMongoDBConnectionString();

        optionsBuilder.UseMongoDB(mongoConnectionString, db);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseAuditableEntity).IsAssignableFrom(entityType.ClrType) || typeof(BaseAuditableEntityWithoutId).IsAssignableFrom(entityType.ClrType))
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
        modelBuilder.Entity<UserReportComment>();
        modelBuilder.Entity<UserReportRecipe>();
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
