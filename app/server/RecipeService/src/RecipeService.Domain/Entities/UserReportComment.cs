using Contract.Constants;
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
    public List<string> ReasonCodes { get; set; } = null!;
    [MaxLength(300)]
    public string? AdditionalDetails { get; set; }
    [Required]
    public ReportStatus Status { get; set; } = ReportStatus.Pending;
}
