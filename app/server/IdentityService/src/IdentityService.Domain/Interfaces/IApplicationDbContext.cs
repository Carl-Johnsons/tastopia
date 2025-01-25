
using Contract.Interfaces;
using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Interfaces;


public interface IApplicationDbContext : IDbContext
{
    DbSet<Permission> Permissions { get; set; }
    DbSet<Group> Groups { get; set; }
    DbSet<RoleGroupPermission> RoleGroupPermissions { get; set; }
}
