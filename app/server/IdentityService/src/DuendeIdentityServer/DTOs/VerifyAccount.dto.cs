using IdentityService.Domain.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace DuendeIdentityServer.DTOs;

public class VerifyAccountDTO
{
    [Required]
    [JsonProperty("OTP")]
    public string OTP { get; set; } = null!;

    [Required]
    [JsonProperty("verifyMethod")]
    [JsonConverter(typeof(StringEnumConverter))]
    public VerifyAccountMethod VerifyMethod { get; set; } = VerifyAccountMethod.Verify;
}
