using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;
public class UpdateCommentDTO
{
    [Required]
    [JsonProperty("commentId")]
    public Guid CommentId { get; set; }

    [Required]
    [JsonProperty("content")]
    public string Content { get; set; } = null!;
}