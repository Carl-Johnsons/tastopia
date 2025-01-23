using Contract.Utilities;
using Microsoft.Extensions.Logging;

namespace UploadFileService.Infrastructure.Persistence;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = EnvUtility.GetConnectionString();
        _logger.LogInformation(connectionString);
        
        optionsBuilder.UseNpgsql(connectionString, option =>
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
    }
}
