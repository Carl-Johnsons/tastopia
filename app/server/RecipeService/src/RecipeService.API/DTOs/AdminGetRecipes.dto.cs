using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.API.DTOs;
public class AdminGetRecipesDTO
{
    [Required]
    [JsonProperty("page")]
    [Range(0, int.MaxValue)]
    public int? Page { get; set; } = null!;

    [JsonProperty("keyword")]
    public string? Keyword { get; set; }
}
