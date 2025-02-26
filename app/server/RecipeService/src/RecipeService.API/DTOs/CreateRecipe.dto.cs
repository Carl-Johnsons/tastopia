using Newtonsoft.Json;
using RecipeService.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class CreateRecipeDTO
{
    [Required]
    [JsonProperty("recipeImage")]
    [MaxFileSize(17)]
    public IFormFile RecipeImage { get; set; } = null!;

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
    [MaxLengthList(50)]
    [NonEmptyList]
    public List<string> Ingredients { get; set; } = null!;

    [Required]
    [JsonProperty("steps")]
    [MaxLengthList(15)]
    [NonEmptyList]
    public List<StepDTO> Steps { get; set; } = null!;

    [JsonProperty("tagValues")]
    public List<string>? TagValues { get; set; }
}

public class StepDTO
{
    [Required]
    [JsonProperty("ordinalNumber")]
    public int OrdinalNumber { get; set; }

    [Required]
    [JsonProperty("content")]
    [MaxLength(500)]
    public string Content { get; set; } = null!;

    [JsonProperty("images")]
    [MaxLengthList(3)]
    [MaxFileSizeList(17)]
    public List<IFormFile>? Images { get; set; } = null!;
}
