using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UserService.API.DTOs;

public class MarkAllUserReportDTO
{
    [Required]
    [JsonProperty("accountId")]
    public Guid AccountId { get; set; }
    [Required]
    [JsonProperty("isReopened")]
    public bool IsReopened { get; set; }
}
