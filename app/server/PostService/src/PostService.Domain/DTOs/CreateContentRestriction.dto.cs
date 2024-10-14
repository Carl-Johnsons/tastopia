using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PostService.Domain.DTOs;

public class CreateContentRestrictionDTO
{
    [Required]
    [JsonProperty("userId")]
    public Guid UserId { get; set; }

    [Required]
    [JsonProperty("typeId")]
    public Guid TypeId { get; set; }

    [Required]
    [JsonProperty("expiredAt")]
    public DateTime ExpiredAt { get; set; }

}
