using Contract.DTOs.UserDTO;
using Newtonsoft.Json;

namespace RecipeService.Domain.Responses;

public class AdminRecipeResponse
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Ingredients { get; set; } = null!;
    public string AuthorAvatarURL { get; set; } = null!;
    public string AuthorDisplayName { get; set; } = null!;
    public string AuthorUsername { get; set; } = null!;
    public bool IsActive { get; set; }
    public string RecipeImageUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class AdminSingleRecipeCommentDetailResponse
{
    public SimpleUser Reporter { get; set; } = null!;
    public AdminRecipeResponse Recipe { get; set; } = null!;
    public SimpleReportResponse Report { get; set; } = null!;
}