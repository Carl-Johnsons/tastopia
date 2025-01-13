using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TrackingService.API.DTOs;

public class GetUserViewRecipeDetailHistoryDTO
{
    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int Skip { get; set; } 
}
