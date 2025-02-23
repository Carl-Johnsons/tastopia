using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TrackingService.API.DTOs;

public class DeleteUserSearchRecipeKeywordDTO
{
    [Required]
    [JsonProperty("keyword")]
    public string Keyword { get; set; } = null!;
}
