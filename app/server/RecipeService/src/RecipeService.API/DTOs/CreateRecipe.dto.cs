using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs
{
    public class CreateRecipeDTO
    {
        [Required]
        [JsonProperty("recipeImage")]
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
        public List<string> Ingredients { get; set; } = null!;

        [Required]
        [JsonProperty("steps")]
        public List<StepDTO> Steps { get; set; } = null!;

        [JsonProperty("tagCodes")]
        public List<string>? TagCodes { get; set; }

        [JsonProperty("additionTagValues")]
        public List<string>? AdditionTagValues { get; set; }
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
        public List<IFormFile>? Images { get; set; } = null!;
    }
}
