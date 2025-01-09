using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Entities;

[Collection("UserBookmarkRecipe")]
public class UserBookmarkRecipe : BaseMongoDBEntity
{
    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public Guid RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
