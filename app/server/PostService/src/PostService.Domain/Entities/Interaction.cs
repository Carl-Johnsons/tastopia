using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PostService.Domain.Entities;

[Table("Interaction")]
[PrimaryKey(nameof(Id))]
public class Interaction : BaseAuditableEntity
{
    [Required]
    [JsonProperty("value")]
    public string Value { get; set; } = null!;

    [Required]
    [JsonProperty("code")]
    public string Code { get; set; } = null!;

    [Required]
    [JsonProperty("gif")]
    public string Gif { get; set; } = null!;
}
