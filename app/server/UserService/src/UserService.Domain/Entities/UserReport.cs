using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

[Table("UserReport")]
public class UserReport : BaseAuditableEntity
{
    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public Guid ReportedId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Reason { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = null!;

    public virtual User? User { get; set; }
    public virtual User? Reported { get; set; }


}
