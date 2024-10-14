using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Domain.Entities;

[Table("PostInteract")]
[PrimaryKey(nameof(PostId), nameof(InteractionId), nameof(UserId))]
public class PostInteract
{
    [Required]
    [JsonProperty("postId")]
    public Guid PostId { get; set; }

    [Required]
    [JsonProperty("interactionId")]
    public Guid InteractionId { get; set; }

    [Required]
    [JsonProperty("userId")]
    public Guid UserId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual Interaction Interaction { get; set; } = null!;

}
