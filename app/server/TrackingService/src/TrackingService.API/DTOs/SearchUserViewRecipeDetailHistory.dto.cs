using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TrackingService.API.DTOs;

public class SearchUserViewRecipeDetailHistoryDTO
{
    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int Skip { get; set; }

    [JsonProperty("keyword")]
    public string? Keyword { get; set; }
}
