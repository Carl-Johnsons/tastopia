using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

[Table("UserFollow")]
[PrimaryKey(nameof(FollowerId), nameof(FollowingId))]
public class UserFollow
{
    [Required]
    public Guid FollowerId { get; set; }
    [Required]
    public Guid FollowingId { get; set; }
    public virtual User? Follower { get; set; }
    public virtual User? Following { get; set; }
}
