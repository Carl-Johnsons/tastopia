using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class GetRecipeStepsDTO
{
    [Required]
    [JsonProperty("recipeId")]
    public Guid RecipeId { get; set; }
}
