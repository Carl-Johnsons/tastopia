using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DuendeIdentityServer.DTOs;

public class DisableAndEnableUserDTO
{
    [JsonProperty("id")]
    [Required]
    public Guid Id { get; set; }
}
