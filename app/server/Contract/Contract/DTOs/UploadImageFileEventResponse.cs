using Contract.Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace Contract.DTOs;

public class UploadImageFileEventResponseDTO : BaseEntity
{
    [Required]
    [MaxLength(200)]
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [Required]
    [JsonProperty("publicId")]
    public string PublicId { get; set; } = null!;

    [Required]
    [JsonProperty("size")]
    public long Size { get; set; }

    [Required]
    [JsonProperty("url")]
    public string Url { get; set; } = null!;
}