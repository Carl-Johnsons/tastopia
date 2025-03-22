using Newtonsoft.Json;
using RecipeService.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;
public class CreateTagDTO
{
    [Required]
    [JsonProperty("code")]
    [MaxLength(50)]
    public string Code { get; set; } = null!;
    [Required]
    [JsonProperty("value")]
    [MaxLength(50)]
    public string Value { get; set; } = null!;
    [Required]
    [JsonProperty("category")]
    [MaxLength(20)]
    [CategoryValidation]
    public string Category { get; set; } = null!;
    [Required]
    [JsonProperty("tagImage")]
    [MaxFileSize(17)]
    public IFormFile TagImage { get; set; } = null!;
}
