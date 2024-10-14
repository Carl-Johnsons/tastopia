using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PostService.Domain.DTOs;

public class UninteractionPostDTO
{
    [Required]
    [JsonProperty("postId")]
    public Guid PostId { get; set; }
}
