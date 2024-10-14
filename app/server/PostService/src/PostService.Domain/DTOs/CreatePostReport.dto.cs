using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PostService.Domain.DTOs;

public class CreatePostReport
{
    [Required]
    [JsonProperty("postId")]
    public Guid PostId { get; set; }

    [Required]
    [JsonProperty("reason")]
    public string Reason { get; set; } = null!;
}
