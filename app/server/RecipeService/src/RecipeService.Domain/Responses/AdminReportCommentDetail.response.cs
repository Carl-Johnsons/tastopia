namespace RecipeService.Domain.Responses;

public class AdminReportCommentDetailResponse
{
    public CommentDetailResponse Comment { get; set; } = null!;
    public AdminRecipeResponse Recipe { get; set; } = null!;
    public List<ReportRecipeResponse> Reports { get; set; } = [];
}

public class CommentDetailResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorUsername { get; set; } = null!;
    public string AuthorDisplayName { get; set; } = null!;
    public string AuthorAvatarURL { get; set; } = null!;
    public string Content { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
