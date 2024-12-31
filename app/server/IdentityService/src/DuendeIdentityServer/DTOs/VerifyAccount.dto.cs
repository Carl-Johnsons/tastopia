using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DuendeIdentityServer.DTOs;

public class VerifyAccountDTO
{
    [Required]
    [JsonProperty("OTP")]
    public string OTP { get; set; } = null!;
}
