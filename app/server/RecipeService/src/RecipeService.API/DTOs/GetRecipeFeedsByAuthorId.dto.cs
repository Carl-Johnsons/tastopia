using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class GetRecipeFeedsByAuthorIdDTO
{
    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int? Skip { get; set; } = null;

    [Required]
    [JsonProperty("authorId")]
    public Guid AuthorId { get; set; }
}

