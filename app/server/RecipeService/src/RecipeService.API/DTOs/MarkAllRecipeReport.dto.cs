using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class MarkAllRecipeReportDTO
{
    [Required]
    [JsonProperty("recipeId")]
    public Guid RecipeId { get; set; }

    [Required]
    [JsonProperty("isReopened")]
    public bool IsReopened { get; set; }
}
