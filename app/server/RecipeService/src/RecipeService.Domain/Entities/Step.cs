using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("Step")]
public class Step : BaseAuditableEntity
{
    [Required]
    public Guid RecipeId { get; set; }
    [Required]
    public int OdinalNumber { get; set; }
    [Required]
    [MaxLength(500)]
    public string Content { get; set; } = null!;

    //Json string url array object
    public List<string>? AttachedImageUrls { get; set; }

    [JsonIgnore]
    public virtual Recipe? Recipe { get; set; }
}
