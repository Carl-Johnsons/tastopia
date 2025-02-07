using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class DeleteOwnRecipe
{
    [Required]
    [JsonProperty("recipeId")]
    public Guid RecipeId { get; set; }
}
