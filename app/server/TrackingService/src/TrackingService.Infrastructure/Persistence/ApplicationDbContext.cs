using Contract.Constants;
using Contract.Utilities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using TrackingService.Domain.Entities;
using TrackingService.Infrastructure.Persistence.Mockup;

namespace TrackingService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ILogger<ApplicationDbContext> _logger;
    public DbContext Instance => this;

    public ApplicationDbContext(ILogger<ApplicationDbContext> logger)
    {
        _logger = logger;
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
        : base(options)
    {
        _logger = logger;
    }
    public DbSet<UserViewRecipeDetail> UserViewRecipeDetails { get; set; }
    public DbSet<UserSearchRecipe> UserSearchRecipes { get; set; }
    public DbSet<UserSearchUser> UserSearchUsers { get; set; }
    public DbSet<AdminActivityLog> AdminActivityLogs { get; set; }

    public async Task SeedDb(IServiceProvider serviceProvider)
    {
        var mockupData = serviceProvider.GetRequiredService<MockupData>();
        await mockupData.SeedAllDataAsync();
        Console.WriteLine("✅ Seed data complete");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        EnvUtility.LoadEnvFile();

        var db = DotNetEnv.Env.GetString("DB", "TrackingDB").Trim();
        var mongoConnectionString = EnvUtility.GetMongoDBConnectionString();

        optionsBuilder.UseMongoDB(mongoConnectionString, db);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseMongoDBAuditableEntity).IsAssignableFrom(entityType.ClrType))
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
        modelBuilder.Entity<AdminActivityLog>(entity =>
        {
            entity.Property(e => e.ActivityType)
                  .HasConversion(typeof(string));
            entity.Property(e => e.EntityType)
                  .HasConversion(typeof(string));
            entity.Property(e => e.SecondaryEntityType)
                  .HasConversion(
                      v => v.HasValue ? v.Value.ToString() : null,  // convert enum to string (or null)
                      v => string.IsNullOrEmpty(v) ? (ActivityEntityType?)null : Enum.Parse<ActivityEntityType>(v) // convert string back to enum
                  );
        });
    }
    public IMongoDatabase GetDatabase()
    {
        EnvUtility.LoadEnvFile();

        var db = DotNetEnv.Env.GetString("DB", "TrackingDB").Trim();
        var mongoConnectionString = EnvUtility.GetMongoDBConnectionString();
        var client = new MongoClient(mongoConnectionString);
        return client.GetDatabase(db);
    }
}
