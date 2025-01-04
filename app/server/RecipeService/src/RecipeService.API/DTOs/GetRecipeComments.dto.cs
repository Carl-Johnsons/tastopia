using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class GetRecipeCommentsDTO
{
    [Required]
    [JsonProperty("recipeId")]
    public Guid RecipeId { get; set; }

    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int? Skip { get; set; } = null;
}
