using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Entities;

[Collection("UserReportComment")]
public class UserReportComment : BaseMongoDBAuditableEntity
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
