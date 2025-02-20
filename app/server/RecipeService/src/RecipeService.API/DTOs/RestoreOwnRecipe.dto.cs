using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class RestoreOwnRecipeDTO
{
    [Required]
    [JsonProperty("recipeId")]
    public Guid RecipeId { get; set; }
}
