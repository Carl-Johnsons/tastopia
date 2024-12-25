using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class GetRecipeFeedsDTO
{
    [Required]
    [JsonProperty("skip")]
    public int Skip { get; set; } = 0;
    [JsonProperty("tagValues")]
    public List<string>? TagValues { get; set; }

}
