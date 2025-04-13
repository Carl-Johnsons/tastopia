using RecipeService.Domain.Entities;

namespace RecipeService.Domain.Responses;

public class AdminReportRecipeDetailResponse
{
    public Recipe Recipe { get; set; } = null!;
    public List<Tag> Tags { get; set; } = [];
    public string AuthorUsername { get; set; } = null!;
    public string AuthorDisplayName { get; set; } = null!;
    public string AuthorAvtUrl { get; set; } = null!;
    public int AuthorNumberOfFollower { get; set; } = 0;
    public List<ReportRecipeResponse> Reports { get; set; } = [];
}

public class ReportRecipeResponse
{
    public Guid Id { get; set; }
    public Guid ReporterId { get; set; }
    public string ReporterUsername { get; set; } = null!;
    public string ReporterDisplayName { get; set; } = null!;
    public string ReporterAvtUrl { get; set; } = null!;
    public List<string> Reasons { get; set; } = [];
    public string? AdditionalDetail { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

public class SimpleReportResponse
{
    public Guid Id { get; set; }
    public Guid ReporterAccountId { get; set; }
    public List<string> Reasons { get; set; } = [];
    public string? AdditionalDetail { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
