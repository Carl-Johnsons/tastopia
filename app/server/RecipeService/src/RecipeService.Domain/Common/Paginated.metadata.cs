using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Common;

public class CommonPaginatedMetadata
{
    [Required]
    [JsonProperty("totalPage")]
    public int TotalPage { get; set; } = 0;

}

public class AdvancePaginatedMetadata : CommonPaginatedMetadata
{
    [Required]
    [JsonProperty("hasNextPage")]
    public bool HasNextPage { get; set; } = true;
}
