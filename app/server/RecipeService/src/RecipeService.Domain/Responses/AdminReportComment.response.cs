namespace RecipeService.Domain.Responses;

public class AdminReportCommentResponse
{
    public Guid ReportId { get; set; }
    public Guid CommentId { get; set; }
    public Guid RecipeId { get; set; }
    public string CommentOwnerUsername { get; set; } = null!;
    public string CommentContent { get; set; } = null!;
    public string RecipeTitle { get; set; } = null!;
    public string RecipeImageURL { get; set; } = null!;
    public string ReporterUsername { get; set; } = null!;
    public string ReportReason { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = null!;
}
