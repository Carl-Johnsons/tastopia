using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PostService.Domain.DTOs;

public class CreateTagDTO
{
    [Required]
    [JsonProperty("value")]
    public string Value { get; set; } = null!;
}
