using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UserService.API.ValidationAttributes;

namespace UserService.API.DTOs;

public class AdminGetUserReportByAccountIdDTO
{
    [Required]
    [JsonProperty("accountId")]
    public Guid AccountId { get; set; }

    [Required]
    [JsonProperty("skip")]
    [Range(0, int.MaxValue)]
    public int? Skip { get; set; } = null!;

    [Required]
    [LanguageValidation]
    [JsonProperty("language")]
    public string Language { get; set; } = null!;
}
