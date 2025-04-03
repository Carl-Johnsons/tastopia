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
    [MaxLength(50)]
    public string Code { get; set; } = null!;
    [Required]
    [JsonProperty("en")]
    [MaxLength(50)]
    public string En { get; set; } = null!;
    [Required]
    [JsonProperty("vi")]
    [MaxLength(50)]
    public string Vi { get; set; } = null!;
    [Required]
    [JsonProperty("status")]
    [TagStatusValidation]
    [MaxLength(20)]
    public string Status { get; set; } = null!;
    [Required]
    [JsonProperty("category")]
    [CategoryValidation]
    [MaxLength(20)]
    public string Category { get; set; } = null!;

    [JsonProperty("tagImage")]
    [MaxFileSize(17)]
    public IFormFile? TagImage { get; set; }
}
