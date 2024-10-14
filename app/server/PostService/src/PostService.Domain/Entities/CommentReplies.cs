using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Domain.Entities;

[Table("CommentReplies")]
[PrimaryKey(nameof(CommentId), nameof(ReplyCommentId))]
public class CommentReplies 
{
    [Required]
    [JsonProperty("commentId")]
    public Guid CommentId { get; set; }

    [Required]
    [JsonProperty("replyCommentId")]
    public Guid ReplyCommentId { get; set; }

    public Comment? Comment {  get; set; }
    public Comment? ReplyComment { get; set; }
}

