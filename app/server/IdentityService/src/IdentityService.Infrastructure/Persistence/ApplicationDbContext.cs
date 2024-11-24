using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationAccount>, IApplicationDbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbContext Instance => this;

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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationAccount>().ToTable("Account");
        builder.Entity<IdentityRole>().ToTable("Role");
        builder.Entity<IdentityUserRole<string>>().ToTable("AccountRole");
        builder.Entity<IdentityUserClaim<string>>().ToTable("AccountClaim");
        builder.Entity<IdentityUserLogin<string>>().ToTable("AccountLogin");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
        builder.Entity<IdentityUserToken<string>>().ToTable("AccountToken");

        builder.Entity<IdentityUserRole<string>>().Property(c => c.UserId).HasColumnName("AccountId");
        builder.Entity<IdentityUserClaim<string>>().Property(c => c.UserId).HasColumnName("AccountId");
        builder.Entity<IdentityUserLogin<string>>().Property(c => c.UserId).HasColumnName("AccountId");
        builder.Entity<IdentityUserToken<string>>().Property(c => c.UserId).HasColumnName("AccountId");

        builder.Entity<ApplicationAccount>(
                e =>
                {
                    e.Property(u => u.Active)
                        .HasDefaultValueSql("True")
                        .HasSentinel(false);

                    e.Property(u => u.EmailConfirmationExpiry)
                          .HasConversion(v => v.ToUniversalTime(),
                                         v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                });
    }
}
