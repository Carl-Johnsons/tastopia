using Newtonsoft.Json;
using RecipeService.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;
public class UpdateTagDTO
{
    [Required]
    [JsonProperty("tagId")]
    public Guid TagId { get; set; }
    [Required]
    [JsonProperty("code")]
    public string Code { get; set; } = null!;
    [Required]
    [JsonProperty("value")]
    public string Value { get; set; } = null!;
    [Required]
    [JsonProperty("status")]
    [TagStatusValidation]
    public string Status { get; set; } = null!;
    [Required]
    [JsonProperty("category")]
    [CategoryValidation]
    public string Category { get; set; } = null!;

    [JsonProperty("tagImage")]
    public IFormFile? TagImage { get; set; }
}
