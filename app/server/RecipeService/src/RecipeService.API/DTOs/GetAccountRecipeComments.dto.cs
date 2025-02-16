using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class GetAccountRecipeCommentsDTO
{
    [Required]
    [JsonProperty("accountId")]
    public Guid AccountId { get; set; }

    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int? Skip { get; set; } = null;
}
