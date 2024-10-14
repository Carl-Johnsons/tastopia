using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UploadFileService.Domain.Common;

namespace UploadFileService.Domain.Entities;

[Table("ExtensionType")]
[PrimaryKey(nameof(Id))]
public class ExtensionType : BaseEntity
{
    [Required]
    [JsonProperty("code")]
    public string Code { get; set; } = null!;

    [Required]
    [JsonProperty("value")]
    public string Value { get; set; } = null!;

    [Required]
    [JsonProperty("type")]
    public string Type { get; set; } = null!;
}
