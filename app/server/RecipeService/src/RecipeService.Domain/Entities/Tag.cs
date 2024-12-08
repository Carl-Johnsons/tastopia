using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("Tag")]
public class Tag : BaseAuditableEntity
{
    [Required]
    public string Value { get; set; } = null!;

    [Required]
    public string Code { get; set; } = null!;

    [Required]
    public string Type { get; set; } = null!;

    public string? ImageUrl { get; set; }
}
