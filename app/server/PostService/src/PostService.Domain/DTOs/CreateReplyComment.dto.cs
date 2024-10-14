using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PostService.Domain.DTOs;

public class CreateReplyCommentDTO
{
    [Required]
    [JsonProperty("postId")]
    public Guid PostId { get; set; }

    [Required]
    [JsonProperty("commentId")]
    public Guid CommentId { get; set; }

    [Required]
    [JsonProperty("content")]
    public string Content { get; set; } = null!;
}
