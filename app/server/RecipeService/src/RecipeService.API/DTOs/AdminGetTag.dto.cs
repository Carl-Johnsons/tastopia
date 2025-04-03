using Contract.ValidationAttributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.API.DTOs;
public class AdminGetTagDTO
{
    [Required]
    [JsonProperty("language")]
    [LanguageValidation]
    public string Language { get; set; } = null!;
}

