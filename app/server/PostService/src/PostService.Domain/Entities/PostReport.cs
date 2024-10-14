using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Domain.Entities;

[Table("PostReport")]
[PrimaryKey(nameof(Id))]
public class PostReport : BaseAuditableEntity
{
    [Required]
    [JsonProperty("postId")]
    public Guid PostId { get; set; }

    [Required]
    [JsonProperty("reason")]
    public string Reason { get; set; } = null!;

    [Required]
    [JsonProperty("userId")]
    public Guid UserId { get; set; }

    public virtual Post Post { get; set; } = null!;

}
