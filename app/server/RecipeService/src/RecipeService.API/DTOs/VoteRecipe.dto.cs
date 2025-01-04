using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;
public class VoteRecipeDTO
{
    [Required]
    [JsonProperty("isUpvote")]
    public bool? IsUpvote { get; set; } = null!;

    [Required]
    [JsonProperty("recipeId")]
    public Guid? RecipeId { get; set; } = null!;
}