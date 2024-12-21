using UploadFileService.Infrastructure.Utilities;

namespace UploadFileService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<CloudinaryFile> CloudinaryFiles { get; set; }
    public DbSet<ExtensionType> ExtensionTypes { get; set; }

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
        modelBuilder.Entity<CloudinaryFile>(entity =>
        {
            entity.HasOne(cf => cf.ExtensionType)
                .WithMany()
                .HasForeignKey(cf => cf.ExtensionTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}
