

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Domain.Entities;

[Table("UserContentRestrictionsType")]
[PrimaryKey(nameof(Id))]
public class UserContentRestrictionsType : BaseEntity
{
    [Required]
    [JsonProperty("code")]
    public string Code { get; set; } = null!;
}
