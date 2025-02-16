using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrackingService.Domain.Entities;

[Collection("UserSearchRecipe")]
public class UserSearchRecipe : BaseMongoDBAuditableEntity
{
    [Required]
    public Guid AccountId { get; set; }
    public string Keyword { get; set; } = null!;
}
