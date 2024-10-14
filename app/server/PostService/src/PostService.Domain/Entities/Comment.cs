using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Domain.Entities;

[Table("Comment")]
[PrimaryKey(nameof(Id))]
public class Comment : BaseAuditableEntity
{
    [Required]
    [JsonProperty("content")]
    public string Content { get; set; } = null!;

    [Required]
    [JsonProperty("userId")]
    public Guid UserId { get; set; }
}
