
using Contract.Interfaces;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain.Interfaces;


public interface IApplicationDbContext : IDbContext
{
    DbSet<IdentityUserLogin<string>> UserLogins { get; set; }
    DbSet<Permission> Permissions { get; set; }
    DbSet<Group> Groups { get; set; }
    DbSet<RoleGroupPermission> RoleGroupPermissions { get; set; }
}
