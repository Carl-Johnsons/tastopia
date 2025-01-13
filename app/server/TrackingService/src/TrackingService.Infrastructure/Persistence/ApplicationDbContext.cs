using Contract.Utilities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MongoDB.Driver;
using TrackingService.Domain.Entities;

namespace TrackingService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<UserViewRecipeDetail> UserViewRecipeDetails { get; set; }

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
