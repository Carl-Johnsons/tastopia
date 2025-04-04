using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.Domain.Responses;
public class AdminTagResponse
{
    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; }
    [Required]
    [JsonProperty("code")]
    public string Code { get; set; } = null!;
    [Required]
    [JsonProperty("en")]
    public string En { get; set; } = null!;
    [Required]
    [JsonProperty("vi")]
    public string Vi { get; set; } = null!;
    [Required]
    [JsonProperty("category")]
    public string Category { get; set; } = null!;
    [Required]
    [JsonProperty("status")]
    public string Status { get; set; } = null!;
    [Required]
    [JsonProperty("imageUrl")]
    public string ImageUrl { get; set; } = null!;
    [Required]
    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }
}
