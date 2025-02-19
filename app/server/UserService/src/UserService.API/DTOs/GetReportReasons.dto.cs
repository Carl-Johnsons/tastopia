using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UserService.API.ValidationAttributes;
namespace UserService.API.DTOs;
public class GetReportReasonsDTO
{
    [Required]
    [LanguageValidation]
    [JsonProperty("language")]
    public string Language { get; set; } = null!;
}

