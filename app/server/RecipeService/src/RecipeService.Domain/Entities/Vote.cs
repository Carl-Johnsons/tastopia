using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("Vote")]
public class Vote : BaseAuditableEntity
{
    [Required]
    public string Value { get; set; } = null!;

    [Required]
    public string Code { get; set; } = null!;

    [Required]
    public string Gif { get; set; } = null!;
}
