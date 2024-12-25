using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Responses;

public class RecipeFeedResponse
{

    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; }
    [Required]
    [JsonProperty("authorId")]
    public Guid AuthorId { get; set; }
    [Required]
    [JsonProperty("title")]
    public string Title { get; set; } = null!;
    [Required]
    [JsonProperty("description")]
    public string Description { get; set; } = null!;
    [Required]
    [JsonProperty("authorDisplayName")]
    public string AuthorDisplayName { get; set; } = null!;
    [Required]
    [JsonProperty("authorAvtUrl")]
    public string AuthorAvtUrl { get; set; } = null!;
    [Required]
    [JsonProperty("voteDiff")]
    public int VoteDiff { get; set; }
    [Required]
    [JsonProperty("numberOfComment")]
    public int NumberOfComment { get; set; } = 0;
}
