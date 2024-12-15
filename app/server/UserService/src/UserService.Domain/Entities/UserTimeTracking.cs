
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

[Table("UserTimeTracking")]
public class UserTimeTracking : BaseAuditableEntity
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public int ActiveSecond { get; set; }
    public virtual User User { get; set; } = null!;

}
