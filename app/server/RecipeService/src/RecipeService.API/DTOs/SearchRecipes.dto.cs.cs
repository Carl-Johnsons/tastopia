using Newtonsoft.Json;
using RecipeService.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class SearchRecipesDTO
{
    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int? Skip { get; set; } = null!;

    [JsonProperty("keyword")]
    public string? Keyword { get; set; }

    [NonEmptyList]
    [JsonProperty("tagCodes")]
    public List<string>? TagCodes { get; set; }

}
