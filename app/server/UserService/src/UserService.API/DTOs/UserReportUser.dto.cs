using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UserService.API.ValidationAttributes;

namespace UserService.API.DTOs;

public class UserReportUserDTO
{
    [Required]
    [JsonProperty("accountId")]
    public Guid AccountId { get; set; }

    [Required]
    [JsonProperty("reasonCodes")]
    [NonEmptyList]
    public List<string> ReasonCodes { get; set; } = null!;
    [JsonProperty("additionalDetails")]
    public string? AdditionalDetails { get; set; }
}
