using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("UserReportComment")]
public class UserReportComment : BaseAuditableEntity
{
    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public Guid CommentId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Reason { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Pending";

    public virtual Comment? Comment { get; set; }
}
