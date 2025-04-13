using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class MarkAllCommentReportDTO
{
    [Required]
    [JsonProperty("recipeId")]
    public Guid RecipeId { get; set; }

    [Required]
    [JsonProperty("commentId")]
    public Guid CommentId { get; set; }

    [Required]
    [JsonProperty("isReopened")]
    public bool IsReopened { get; set; }
}
