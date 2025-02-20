using Newtonsoft.Json;
using RecipeService.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class UserReportRecipeDTO
{
    [Required]
    [JsonProperty("recipeId")]
    public Guid RecipeId { get; set; }
    [Required]
    [JsonProperty("reasonCodes")]
    [NonEmptyList]
    public List<string> ReasonCodes { get; set; } = null!;
    [JsonProperty("additionalDetails")]
    [MaxLength(1000)]
    public string? AdditionalDetails { get; set; }
}
