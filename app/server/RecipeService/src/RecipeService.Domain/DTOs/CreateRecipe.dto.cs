using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.DTOs
{
    public class CreateRecipeDTO
    {
        [Required]
        [JsonProperty("recipe-image")]
        public IFormFile RecipeImage { get; set; } = null!;

        [Required]
        [JsonProperty("title")]
        [MaxLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [JsonProperty("description")]
        [MaxLength(50)]
        public string Description { get; set; } = null!;
    }
}
