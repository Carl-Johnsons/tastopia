using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PostService.Domain.DTOs;

public class UpdateCommentDTO
{
    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [Required]
    [JsonProperty("content")]
    public string Content { get; set; } = null!;
}
