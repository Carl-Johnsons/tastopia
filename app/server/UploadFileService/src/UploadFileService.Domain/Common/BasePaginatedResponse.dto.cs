using Newtonsoft.Json;

namespace UploadFileService.Domain.Common;

public class BasePaginatedResponse<Type, MetadataType>
    where Type : class
    where MetadataType : class
{
    [JsonProperty("paginatedData")]
    public IEnumerable<Type> PaginatedData { get; set; } = [];

    [JsonProperty("metadata")]
    public MetadataType? Metadata { get; set; } = null!;
}

