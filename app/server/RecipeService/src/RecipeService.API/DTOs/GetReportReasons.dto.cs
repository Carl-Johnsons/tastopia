using Contract.ValidationAttributes;
using Newtonsoft.Json;
using RecipeService.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.API.DTOs;
public class GetReportReasonsDTO
{
    [Required]
    [ReportTypeValidation]
    [JsonProperty("reportType")]
    public string ReportType { get; set; } = null!;

    [Required]
    [LanguageValidation]
    [JsonProperty("language")]
    public string Language { get; set; } = null!;
}

