using Contract.Constants;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.API.DTOs;
public class ReportDTO
{
    [Required]
    [JsonProperty("reportId")]
    public Guid ReportId { get; set; }
}
