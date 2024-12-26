using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class GetRecipeDetail
{
    [Required]
    [JsonProperty("recipeId")]
    public string RecipeId { get; set; } = null!;
}
