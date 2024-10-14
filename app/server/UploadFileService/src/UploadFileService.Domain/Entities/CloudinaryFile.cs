
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UploadFileService.Domain.Common;

namespace UploadFileService.Domain.Entities;

[Table("CloudinaryFile")]
[PrimaryKey(nameof(Id))]
public class CloudinaryFile : BaseEntity
{
    [Required]
    [JsonProperty("publicId")]
    public string PublicId { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [Required]
    [JsonProperty("extensionTypeId")]
    public Guid ExtensionTypeId { get; set; }

    [Required]
    [JsonProperty("size")]
    public long Size { get; set; }

    [Required]
    [JsonProperty("url")]
    public string Url { get; set; } = null!;

    //navigation props
    public virtual ExtensionType ExtensionType { get; set; } = null!;
}
