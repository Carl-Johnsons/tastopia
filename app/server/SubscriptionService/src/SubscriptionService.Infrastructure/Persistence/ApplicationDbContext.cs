using Contract.Utilities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SubscriptionService.Domain.Entities;
using BaseAuditableEntity = SubscriptionService.Domain.Common.BaseAuditableEntity;

namespace SubscriptionService.Infrastructure.Persistence;

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

    public DbSet<Event> Events { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<SubscriptionEvent> SubscriptionEvents { get; set; }
    public DbSet<UserSubscription> UserSubscriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(EnvUtility.GetConnectionString(), option =>
        {
            option.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(15),
                    errorCodesToAdd: null
                );
        });
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

        modelBuilder.Entity<Event>(e =>
        {
            e.Property(evt => evt.StartDate)
                .HasConversion(v => v.ToUniversalTime(),
                               v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            e.Property(evt => evt.EndDate)
                .HasConversion(v => v.ToUniversalTime(),
                               v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        });

        modelBuilder.Entity<SubscriptionEvent>(se =>
        {
            se.HasOne(se => se.Subscription)
                .WithMany()
                .HasForeignKey(se => se.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            se.HasOne(se => se.Event)
                .WithMany()
                .HasForeignKey(se => se.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserSubscription>(us =>
        {
            us.HasOne(us => us.Subscription)
                .WithMany()
                .HasForeignKey(us => us.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            us.HasOne(us => us.Payment)
                .WithMany()
                .HasForeignKey(us => us.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
