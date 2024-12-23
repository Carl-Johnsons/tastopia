using Newtonsoft.Json;

namespace DuendeIdentityServer.DTOs;

public class GetApplicationUserDTO
{
    [JsonProperty("id")]
    public string? Id { get; set; }
}
