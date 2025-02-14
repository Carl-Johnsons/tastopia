using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class UserReportRecipeDTO
{
    [Required]
    [JsonProperty("recipeId")]
    public Guid RecipeId { get; set; }

    [Required]
    [JsonProperty("reason")]
    public string Reason { get; set; } = null!;
}
