using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.API.DTOs;
public class CreateUserSearchRecipeDTO
{
    [Required]
    [JsonProperty("keyword")]
    public string Keyword { get; set; } = null!;
}
