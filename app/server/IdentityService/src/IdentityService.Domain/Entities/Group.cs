using IdentityService.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.Domain.Entities;

[Table("Group")]
public class Group : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
}
