using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UserService.API.DTOs;

public class UserReportUserDTO
{
    [Required]
    [JsonProperty("accountId")]
    public Guid AccountId { get; set; }

    [Required]
    [JsonProperty("reason")]
    public string Reason { get; set; } = null!;
}
