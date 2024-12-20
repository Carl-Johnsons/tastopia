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
    public string Category { get; set; } = null!;

    [Required]
    public string Status { get; set; } = "Pending";

    [Required]
    public bool IsActive { get; set; } = false;

    public string? ImageUrl { get; set; }
}
