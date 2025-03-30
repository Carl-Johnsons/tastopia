using Contract.ValidationAttributes;
using Newtonsoft.Json;
using RecipeService.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.API.DTOs;
public class AdminGetUserActivityDTO
{
    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int? Skip { get; set; } = null!;

    [Required]
    [LanguageValidation]
    [JsonProperty("language")]
    public string Language { get; set; } = null!;

    [Required]
    [JsonProperty("accountId")]
    public Guid AccountId { get; set; }
}

