using Contract.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

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
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<RoleGroupPermission> RoleGroupPermissions { get; set; }

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
                    e.Property(a => a.IsActive)
                        .HasDefaultValueSql("True")
                        .HasSentinel(false);

                    e.Property(a => a.EmailConfirmed)
                        .HasDefaultValueSql("False")
                        .HasSentinel(false);

                    e.Property(a => a.PhoneNumberConfirmed)
                       .HasDefaultValueSql("False")
                       .HasSentinel(false);

                    e.Property(a => a.IsFirstTimeLogin)
                        .HasDefaultValueSql("True")
                        .HasSentinel(false);

                    ApplyUtcConversionForDateTimeNullable(e, a => a.EmailOTPCreated);
                    ApplyUtcConversionForDateTimeNullable(e, a => a.EmailOTPExpiry);
                    ApplyUtcConversionForDateTimeNullable(e, a => a.PhoneOTPCreated);
                    ApplyUtcConversionForDateTimeNullable(e, a => a.PhoneOTPExpiry);
                });

        builder.Entity<RoleGroupPermission>(e =>
        {
            e.HasOne(rgp => rgp.Role)
                .WithMany()
                .HasForeignKey(rgp => rgp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(rp => rp.Permission)
                .WithMany()
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(rp => rp.Group)
                .WithMany()
                .HasForeignKey(rp => rp.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }


    private void ApplyUtcConversionForDateTimeNullable<T>(EntityTypeBuilder<T> e, Expression<Func<T, DateTime?>> propertyExpression) where T : class
    {
        e.Property(propertyExpression)
         .HasConversion(
             v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null,
             v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null
         );
    }

}
