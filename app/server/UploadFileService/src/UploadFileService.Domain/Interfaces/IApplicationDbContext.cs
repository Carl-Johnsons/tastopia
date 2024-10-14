using Microsoft.EntityFrameworkCore;
using UploadFileService.Domain.Entities;

namespace UploadFileService.Domain.Interfaces;


public interface IApplicationDbContext : IDbContext
{
    DbSet<CloudinaryFile> CloudinaryFiles { get; set; }
    DbSet<ExtensionType> ExtensionTypes { get; set; }
}
