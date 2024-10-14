using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PostService.Domain.DTOs;

public class CreateInteractionDTO
{
    [Required]
    [JsonProperty("value")]
    public string Value { get; set; } = null!;

    [Required]
    [JsonProperty("gif")]
    public string Gif { get; set; } = null!;

    [Required]
    [JsonProperty("code")]
    public string Code { get; set; } = null!;
}
