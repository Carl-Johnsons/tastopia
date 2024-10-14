
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Domain.Entities;

[Table("PostComment")]
[PrimaryKey(nameof(PostId), nameof(CommentId))]
public class PostComment
{
    [Required]
    [JsonProperty("postId")]
    public Guid PostId { get; set; }

    [Required]
    [JsonProperty("commentId")]
    public Guid CommentId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual Comment Comment { get; set; } = null!;
}
