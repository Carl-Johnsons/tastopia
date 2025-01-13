using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TrackingService.API.DTOs;

public class GetUserViewRecipeDetailHistoryDTO
{
    [Required]
    [JsonProperty("skip")]
    public int Skip {  get; set; }
}
