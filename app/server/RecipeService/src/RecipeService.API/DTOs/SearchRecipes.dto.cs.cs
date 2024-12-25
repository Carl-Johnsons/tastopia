using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class SearchRecipesDTO
{
    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int Skip { get; set; } = 0;

    [JsonProperty("keyword")]
    public string? Keyword { get; set; }

    [JsonProperty("tagValues")]
    public List<string>? TagValues { get; set; }

}
