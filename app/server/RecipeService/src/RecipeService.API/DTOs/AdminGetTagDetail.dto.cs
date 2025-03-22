using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.API.DTOs;
public class AdminGetTagDetailDTO
{
    [Required]
    [JsonProperty("tagId")]
    public Guid TagId { get; set; }
}

