using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Responses;

public class AdminRecipeResponse
{
    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; }
    [Required]
    [JsonProperty("authorId")]
    public Guid AuthorId { get; set; }
    [Required]
    [JsonProperty("title")]
    public string Title { get; set; } = null!;
    [Required]
    [JsonProperty("ingredients")]
    public string Ingredients { get; set; } = null!;
    [Required]
    [JsonProperty("authorDisplayName")]
    public string AuthorDisplayName { get; set; } = null!;
    [Required]
    [JsonProperty("authorUsername")]
    public string AuthorUsername { get; set; } = null!;
    [Required]
    [JsonProperty("isActive")]
    public bool IsActive { get; set; }
    [Required]
    [JsonProperty("recipeImgUrl")]
    public string RecipeImageUrl { get; set; } = null!;
    [Required]
    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }
}
