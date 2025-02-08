using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.API.DTOs;

public class UpdateRecipeDTO
{
    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("recipeImage")]
    public IFormFile? RecipeImage { get; set; }

    [Required]
    [JsonProperty("title")]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    [Required]
    [JsonProperty("description")]
    [MaxLength(500)]
    public string Description { get; set; } = null!;

    [JsonProperty("serves")]
    public int? Serves { get; set; }

    [JsonProperty("cookTime")]
    public string? CookTime { get; set; }

    [Required]
    [JsonProperty("ingredients")]
    public List<string> Ingredients { get; set; } = null!;

    [Required]
    [JsonProperty("steps")]
    public List<UpdateStepDTO> Steps { get; set; } = null!;

    [JsonProperty("tagValues")]
    public List<string>? TagValues { get; set; }
}

public class UpdateStepDTO
{
    [Required]
    [JsonProperty("stepId")]
    public Guid StepId { get; set; }

    [Required]
    [JsonProperty("ordinalNumber")]
    public int OrdinalNumber { get; set; }

    [Required]
    [JsonProperty("content")]
    [MaxLength(500)]
    public string Content { get; set; } = null!;

    [JsonProperty("images")]
    public List<IFormFile>? Images { get; set; }

    [JsonProperty("deleteUrls")]
    public List<string>? DeleteUrls { get; set; }
}
