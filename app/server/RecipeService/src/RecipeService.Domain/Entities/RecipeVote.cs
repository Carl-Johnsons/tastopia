using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Entities;

[Collection("RecipeVote")]
public class RecipeVote : BaseEntity
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public bool IsUpvote { get; set; } = true;
    public virtual Recipe? Recipe { get; set; }
}
