using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RecipeService.Domain.Responses;
public class UserActivityResponse
{
    [JsonConverter(typeof(StringEnumConverter))]
    public UserActivityType Type {  get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string TimeAgo { get; set; } = null!;
    public DateTime Time {  get; set; }
    public string? Username { get; set; }
    public Guid? AccountId { get; set; }
    public string? AvtImageUrl { get; set; }
    public Guid? RecipeId { get; set; }
    public string? RecipeTitle { get; set; }
    public string? RecipeAuthorUsername { get; set; }
    public Guid? RecipeAuthorId { get; set; }
    public string? RecipeImageUrl { get; set; }
    public string? RecipeTimeAgo { get; set; }
    public DateTime? RecipeTime { get; set; }
    public int? RecipeVoteDiff { get; set; }
    public Guid? CommentId { get; set; }
    public string? CommentContent { get; set; }
}

public enum UserActivityType
{
    CreateRecipe,
    CommentRecipe,
    UpvoteRecipe,
    DownvoteRecipe,
    Banned
}
