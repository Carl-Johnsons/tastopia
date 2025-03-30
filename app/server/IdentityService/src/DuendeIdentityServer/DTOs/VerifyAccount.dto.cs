using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DuendeIdentityServer.DTOs;

public class VerifyAccountDTO
{
    [Required]
    [JsonProperty("OTP")]
    public string OTP { get; set; } = null!;
}
public class VerifyUpdateIdentifierDTO
{
    [Required]
    [JsonProperty("OTP")]
    public string OTP { get; set; } = null!;
    [Required]
    [JsonProperty("identifier")]
    public string Identifier { get; set; } = null!;
}