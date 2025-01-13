using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UploadFileService.Domain.Responses;
public class FileResponse
{
    [Required]
    [JsonProperty("publicId")]
    public string PublicId { get; set; } = null!;

    [Required]
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [Required]
    [JsonProperty("extension")]
    public string Extension { get; set; } = null!;

    [Required]
    [JsonProperty("size")]
    public long Size { get; set; }

    [Required]
    [JsonProperty("url")]
    public string Url { get; set; } = null!;
}
