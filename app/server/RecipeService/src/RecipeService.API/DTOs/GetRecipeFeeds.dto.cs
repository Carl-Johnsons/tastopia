using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class GetRecipeFeedsDTO
{
    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int Skip { get; set; } = 0;
    [JsonProperty("tagValues")]
    public List<string>? TagValues { get; set; }

}
