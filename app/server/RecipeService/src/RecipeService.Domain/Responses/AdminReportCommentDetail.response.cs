namespace RecipeService.Domain.Responses;

public class AdminReportCommentDetailResponse
{
    public Guid CommentId { get; set; }
    public string CommentOwnerUsername { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string CommentContent { get; set; } = null!;
    public AdminRecipeResponse Recipe { get; set; } = null!;
    public List<ReportRecipeResponse> Reports { get; set; } = [];
}
