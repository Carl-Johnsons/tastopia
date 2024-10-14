using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Domain.Entities;

[Table("UserWarning")]
[PrimaryKey(nameof(Id))]
public class UserWarning : BaseAuditableEntity
{
    [Required]
    [JsonProperty("userId")]
    public Guid UserId { get; set; }

    [Required]
    [JsonProperty("warningCount")]
    public int WarningCount { get; set; } = 0;
}
