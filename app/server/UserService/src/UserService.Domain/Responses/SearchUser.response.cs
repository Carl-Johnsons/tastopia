using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Responses;

public class SearchUserResponse
{
    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [Required]
    [JsonProperty("avtUrl")]
    public string AvtUrl { get; set; } = null!;

    [Required]
    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = null!;

    [Required]
    [JsonProperty("username")]
    public string Username { get; set; } = null!;

    [Required]
    [JsonProperty("numberOfRecipe")]
    public int NumberOfRecipe { get; set; } = 0;

    [Required]
    [JsonProperty("isFollowing")]
    public bool IsFollowing { get; set; } = false;
}
