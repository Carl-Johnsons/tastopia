using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PostService.Domain.DTOs
{
    public class UpdatePostDTO
    {
        [Required]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("content")]
        public string? Content { get; set; } = null!;

        [JsonProperty("tagIds")]
        public List<Guid>? TagIds { get; set; } = null!;

        [JsonProperty("active")]
        public bool? Active { get; set; }
    }
}
