using Newtonsoft.Json;
using RecipeService.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.API.DTOs;
public class UserReportCommentDTO
{
    [Required]
    [JsonProperty("commentId")]
    public Guid CommentId { get; set; }
    [Required]
    [JsonProperty("reasonCodes")]
    [NonEmptyList]
    public List<string> ReasonCodes { get; set; } = null!;
    [JsonProperty("additionalDetails")]
    public string? AdditionalDetails { get; set; }
}
