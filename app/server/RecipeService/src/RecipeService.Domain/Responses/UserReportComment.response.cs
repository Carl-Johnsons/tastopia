using RecipeService.Domain.Entities;
namespace RecipeService.Domain.Responses;

public class UserReportCommentResponse
{
    public UserReportComment Report { get; set; } = null!;
    public bool IsRemoved { get; set; }
}
