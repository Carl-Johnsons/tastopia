using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeService.Domain.Entities;
using RecipeService.Infrastructure.Utilities;

namespace RecipeService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Tag> Tags { get; set; }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
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
        modelBuilder.Entity<Recipe>();
        modelBuilder.Entity<Tag>();
        modelBuilder.Entity<UserReportComment>();
        modelBuilder.Entity<UserReportRecipe>();

        modelBuilder.Entity<RecipeTag>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeTags)
            .HasForeignKey(ri => ri.RecipeId);

    }
}
