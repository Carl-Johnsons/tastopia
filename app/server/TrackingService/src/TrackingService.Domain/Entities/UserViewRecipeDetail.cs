using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrackingService.Domain.Entities;

[Collection("UserViewRecipeDetail")]
public class UserViewRecipeDetail : BaseMongoDBAuditableEntity
{
    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public Guid RecipeId { get; set; }

}
