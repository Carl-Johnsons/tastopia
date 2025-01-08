using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Entities;

[Collection("RecipeTag")]
public class RecipeTag : BaseEntity
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public Guid TagId { get; set; }

    public virtual Recipe? Recipe { get; set; }
    public virtual Tag? Tag { get; set; }

}
