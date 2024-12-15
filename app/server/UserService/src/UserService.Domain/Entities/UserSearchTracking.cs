using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

[Table("UserSearchTracking")]
public class UserSearchTracking : BaseAuditableEntity
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string KeyWord { get; set; } = null!;
    public virtual User User { get; set; } = null!;

}
