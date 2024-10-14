using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PostService.Domain.DTOs;

public class DeleteContentRestrictionDTO
{
    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; }
}
