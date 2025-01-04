using Newtonsoft.Json;
using RecipeService.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class GetRecipeFeedsDTO
{
    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int? Skip { get; set; } = null;

    [Required]
    [NonEmptyList]
    [JsonProperty("tagValues")]
    public List<string> TagValues { get; set; } = null!;
}

