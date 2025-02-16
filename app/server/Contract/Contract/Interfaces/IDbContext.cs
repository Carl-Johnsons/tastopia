using Microsoft.EntityFrameworkCore;

namespace Contract.Interfaces;
public interface IDbContext : IDisposable
{
    DbContext Instance { get; }
}
