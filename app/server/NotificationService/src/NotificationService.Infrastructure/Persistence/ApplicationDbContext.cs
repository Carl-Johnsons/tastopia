using Contract.Utilities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MongoDB.Driver;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext()
    {
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbContext Instance => this;

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
    public DbSet<AccountExpoPushToken> AccountExpoPushTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        EnvUtility.LoadEnvFile();
        var db = DotNetEnv.Env.GetString("DB", "NotificationDB").Trim();
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
        modelBuilder.Entity<AccountExpoPushToken>(e =>
        {
            e.Property(e => e.DeviceType)
             .HasConversion<string>();
        });
        modelBuilder.Entity<Notification>(e =>
        {
            e.OwnsMany(n => n.PrimaryActors, a =>
            {
                a.Property(actor => actor.Type)
                 .HasConversion<string>(); // Convert enum to string
            });

            e.OwnsMany(n => n.SecondaryActors, a =>
            {
                a.Property(actor => actor.Type)
                 .HasConversion<string>(); // Convert enum to string
            });
        });

        modelBuilder.Entity<NotificationTemplate>(nt =>
        {
            nt.Property(e => e.TemplateCode)
              .HasConversion<string>();
        });
    }

    public IMongoDatabase GetDatabase()
    {
        EnvUtility.LoadEnvFile();

        var db = DotNetEnv.Env.GetString("DB", "NotificationDB").Trim();
        var mongoConnectionString = EnvUtility.GetMongoDBConnectionString();
        var client = new MongoClient(mongoConnectionString);
        return client.GetDatabase(db);
    }
}
