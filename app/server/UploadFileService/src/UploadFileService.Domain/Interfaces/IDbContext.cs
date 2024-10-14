using Microsoft.EntityFrameworkCore;

namespace UploadFileService.Domain.Interfaces;
public interface IDbContext : IDisposable
{
    DbContext Instance { get; }
}
