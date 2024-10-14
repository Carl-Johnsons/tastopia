using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Domain.Entities;

[Table("Post")]
[PrimaryKey(nameof(Id))]
public class Post : BaseAuditableEntity
{
    [Required]
    [JsonProperty("content")]
    public string Content { get; set; } = null!;

    [Required]
    [JsonProperty("active")]
    public bool Active { get; set; } = true;

    [JsonProperty("attachedFilesURL")]
    public string AttachedFilesURL { get; set; } = "[]";

    [Required]
    [JsonProperty("userId")]
    public Guid UserId { get; set; }
}
