using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Common;

public class CommonPaginatedMetadata
{
    [Required]
    [JsonProperty("totalPage")]
    public int TotalPage { get; set; } = 0;
}