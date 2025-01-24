using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RecipeService.Domain.Constants;

namespace RecipeService.Domain.Responses;

public class VoteResponse
{
    public Guid AccountId { get; set; }
    public Guid RecipeId { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public Vote Vote { get; set; }
}
