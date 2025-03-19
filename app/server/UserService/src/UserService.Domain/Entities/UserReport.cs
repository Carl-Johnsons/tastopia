using Contract.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

[Table("UserReport")]
public class UserReport : BaseAuditableEntity
{
    [Required]
    public Guid ReportedId { get; set; }

    [Required]
    public Guid ReporterId { get; set; }
    [Required]
    public List<string> ReasonCodes { get; set; } = null!;

    [MaxLength(300)]
    public string? AdditionalDetails { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public ReportStatus Status { get; set; } = ReportStatus.Pending;

    public virtual User? Reported { get; set; }
    public virtual User? Reporter { get; set; }


}
