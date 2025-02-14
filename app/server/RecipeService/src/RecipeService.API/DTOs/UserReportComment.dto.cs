using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class UserReportCommentDTO
{
    [Required]
    [JsonProperty("commentId")]
    public Guid CommentId { get; set; }

    [Required]
    [JsonProperty("reason")]
    public string Reason { get; set; } = null!;
}
