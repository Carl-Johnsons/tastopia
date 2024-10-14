using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PostService.Domain.DTOs;

public class DeletePostDTO
{
    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; }
}
