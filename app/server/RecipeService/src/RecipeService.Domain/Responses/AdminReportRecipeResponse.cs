namespace RecipeService.Domain.Responses;

public class AdminReportRecipeResponse
{
    public Guid RecipeId { get; set; }
    public string RecipeTitle { get; set; } = null!;
    public string RecipeOwnerUsername { get; set; } = null!;
    public string? RecipeImageURL { get; set; }
    public string ReporterUsername { get; set; } = null!;
    public string ReportReason { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = null!;
}
