using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace UserService.API.DTOs;

public class GetUserFollowersDTO
{
    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int? Skip { get; set; } = null!;

    [JsonProperty("keyword")]
    public string Keyword { get; set; } = null!;
}
