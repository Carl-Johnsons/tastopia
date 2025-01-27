using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IdentityService.Domain.Entities;

[Table("RoleGroupPermission")]
[PrimaryKey(nameof(RoleId), nameof(PermissionId))]
public class RoleGroupPermission : BaseEntity
{
    [Required]
    public string RoleId { get; set; } = null!;
    [Required]
    public Guid PermissionId { get; set; }
    [Required]
    public Guid GroupId { get; set; }

    public virtual IdentityRole? Role { get; set; }
    public virtual Permission? Permission { get; set; }
    public virtual Group? Group { get; set; }
}
