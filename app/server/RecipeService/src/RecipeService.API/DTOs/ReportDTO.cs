using Contract.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RecipeService.API.DTOs;

public class ReportDTO
{
    public Guid ReportId { get; set; }

    [JsonProperty]
    [JsonConverter(typeof(StringEnumConverter))]
    public ReportType ReportType { get; set; }
}
