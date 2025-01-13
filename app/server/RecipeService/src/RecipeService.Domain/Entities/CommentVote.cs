using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Entities;

[Collection("CommentVote")]
public class CommentVote : BaseMongoDBEntity
{
    [Required]
    public Guid CommentId { get; set; }

    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public bool IsUpvote { get; set; } = true;

    public virtual Comment? Comment { get; set; }

}
