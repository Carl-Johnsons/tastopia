using Contract.Constants;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Entities;

[Collection("UserReportRecipe")]
public class UserReportRecipe : BaseMongoDBAuditableEntity
{
    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Reason { get; set; } = null!;

    [Required]
    public ReportStatus Status { get; set; } = ReportStatus.Pending;
}
