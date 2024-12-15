using IdentityService.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;


namespace IdentityService.Domain.Entities;

[Table("Permission")]
public class Permission : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string? Description { get; set; } = null!;
}
