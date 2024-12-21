using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DuendeIdentityServer.DTOs;

public class UnblockUserDTO
{
    [Required]
    [JsonProperty("unblockUserId")]
    public string UnblockUserId { get; set; } = null!;
}
