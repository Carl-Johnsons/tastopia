using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;
public class CommentRecipeDTO
{
    [Required]
    [JsonProperty("recipeId")]
    public Guid? RecipeId { get; set; } = null!;
    [Required]
    [JsonProperty("content")]
    public string? Content { get; set; } = null!;
}