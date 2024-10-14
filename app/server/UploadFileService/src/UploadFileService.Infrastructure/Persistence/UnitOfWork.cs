
namespace UploadFileService.Infrastructure.Persistence;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        var addedEntities = _context.ChangeTracker.Entries()
                                                .Where(e => e.State == EntityState.Added)
                                                .ToList();

        var modifiedEntities = _context.ChangeTracker.Entries()
                                            .Where(e => e.State == EntityState.Modified)
                                            .ToList();

        var currentTime = DateTime.Now;

        foreach (var entry in addedEntities)
        {
            if (entry.Properties.Any(p => p.Metadata.Name == "CreatedAt"))
            {
                entry.Property("CreatedAt").CurrentValue = currentTime;
            }

            if (entry.Properties.Any(p => p.Metadata.Name == "UpdatedAt"))
            {
                entry.Property("UpdatedAt").CurrentValue = currentTime;
            }
        }

        foreach (var entry in modifiedEntities)
        {
            if (entry.Properties.Any(p => p.Metadata.Name == "UpdatedAt"))
            {
                entry.Property("UpdatedAt").CurrentValue = currentTime;
            }
        }


        return _context.SaveChangesAsync(cancellationToken);
    }

}
