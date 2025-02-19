using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;
public class DeleteCommentDTO
{
    [Required]
    [JsonProperty("commentId")]
    public Guid CommentId { get; set; }
}