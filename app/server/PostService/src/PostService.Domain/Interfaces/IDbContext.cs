namespace PostService.Domain.Interfaces;
public interface IDbContext : IDisposable
{
    DbContext Instance { get; }
}
