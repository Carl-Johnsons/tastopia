using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class GetUserRecipeBinsDTO
{
    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int? Skip { get; set; } = null!;
}
