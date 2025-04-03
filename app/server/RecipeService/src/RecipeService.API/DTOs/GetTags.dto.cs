using Contract.ValidationAttributes;
using Newtonsoft.Json;
using RecipeService.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;
public class GetTagsDTO
{
    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int? Skip { get; set; } = null!;

    [JsonProperty("keyword")]
    public string? Keyword { get; set; }

    [JsonProperty("tagCodes")]
    public List<string> TagCodes { get; set; } = null!;

    [Required]
    [JsonProperty("category")]
    [CategoryValidation]
    public string Category { get; set; } = null!;

    [Required]
    [JsonProperty("language")]
    [LanguageValidation]
    public string Language { get; set; } = null!;
}
