using Newtonsoft.Json;

namespace NotificationService.Domain.Common;

public class BasePaginatedResponse<Type, MetadataType>
    where Type : class
    where MetadataType : class
{
    [JsonProperty("paginatedData")]
    public IEnumerable<Type> PaginatedData { get; set; } = [];

    [JsonProperty("metadata")]
    public MetadataType? Metadata { get; set; } = null!;
}

