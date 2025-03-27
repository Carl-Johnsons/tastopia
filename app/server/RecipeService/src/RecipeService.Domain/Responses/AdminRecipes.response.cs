using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Responses;

public class AdminRecipeResponse
{
    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; }
    [Required]
    public Guid AuthorId { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Ingredients { get; set; } = null!;
    [Required]
    public string AuthorAvatarURL { get; set; } = null!;
    [Required]
    public string AuthorDisplayName { get; set; } = null!;
    [Required]
    public string AuthorUsername { get; set; } = null!;
    [Required]
    public bool IsActive { get; set; }
    [Required]
    public string RecipeImageUrl { get; set; } = null!;
    [Required]
    public DateTime CreatedAt { get; set; }
}
