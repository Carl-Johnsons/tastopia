using Contract.ValidationAttributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace DuendeIdentityServer.DTOs;
public class AdminGetAccountStatisticDTO
{
    [Required]
    [StatisticRangeValidation]
    [JsonProperty("rangeType")]
    public string RangeType { get; set; } = null!;

    [Required]
    [LanguageValidation]
    [JsonProperty("language")]
    public string Language { get; set; } = null!;
}