using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;
public class CommentRecipeDTO
{
    [Required]
    [JsonProperty("recipeId")]
    [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Cannot use default Guid")]
    public Guid RecipeId { get; set; } = Guid.Empty;
    [Required]
    [JsonProperty("content")]
    public string Content { get; set; } = null!;
}