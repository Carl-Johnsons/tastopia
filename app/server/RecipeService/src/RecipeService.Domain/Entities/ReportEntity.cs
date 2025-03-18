using Contract.Constants;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.Domain.Entities;

public class CommonReportEntity : BaseMongoDBAuditableEntity
{
    [Required]
    public Guid AccountId { get; set; }
    [Required]
    public Guid EntityId { get; set; }
    [Required]
    public List<string> ReasonCodes { get; set; } = null!;
    [MaxLength(300)]
    public string? AdditionalDetails { get; set; }
    [Required]
    public ReportStatus Status { get; set; } = ReportStatus.Pending;
}


[Collection("UserReportRecipe")]
public class UserReportRecipe : CommonReportEntity { }

[Collection("UserReportComment")]
public class UserReportComment : CommonReportEntity
{
    public Guid RecipeId { get; set; }
}
