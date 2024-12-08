using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IdentityService.Domain.Entities;

[Table("RolePermission")]
[PrimaryKey(nameof(RoleId), nameof(PermissionId))]
public class RolePermission
{
    [Required]
    public string RoleId { get; set; } = null!;
    [Required]
    public Guid PermissionId { get; set; }

    public virtual IdentityRole? Role { get; set; }
    public virtual Permission? Permission { get; set; }
}
