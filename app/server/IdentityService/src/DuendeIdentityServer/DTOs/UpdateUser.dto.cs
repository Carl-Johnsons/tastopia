using Newtonsoft.Json;
namespace DuendeIdentityServer.DTOs;

public class UpdateUserDTO
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("avatarFile")]
    public IFormFile? AvatarFile { get; set; }

    [JsonProperty("backgroundFile")]
    public IFormFile? BackgroundFile { get; set; }

    [JsonProperty("introduction")]
    public string? Introduction { get; set; }

    [JsonProperty("gender")]
    public string? Gender { get; set; }

    [JsonProperty("dob")]
    public DateTime? Dob { get; set; }

}
