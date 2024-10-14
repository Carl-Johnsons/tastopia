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
        DotNetEnv.Env.Load();
        var server = DotNetEnv.Env.GetString("DB_SERVER", "localhost, 2001").Trim();
        var db = DotNetEnv.Env.GetString("DB", "Not found").Trim();
        var pwd = DotNetEnv.Env.GetString("SA_PASSWORD", "Not found").Trim();

        var connectionString = $"Server={server};Database={db};User Id=sa;Password='{pwd}';TrustServerCertificate=true;MultipleActiveResultSets=True";
        Console.WriteLine(connectionString);
        optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
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
