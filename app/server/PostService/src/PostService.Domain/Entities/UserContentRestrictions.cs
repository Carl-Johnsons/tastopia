using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PostService.Domain.Entities;

public class UserContentRestrictions : BaseAuditableEntity
{
    [Required]
    [JsonProperty("userId")]
    public Guid UserId { get; set; }

    [Required]
    [JsonProperty("userContentRestrictionsTypeId")]
    public Guid UserContentRestrictionsTypeId { get; set; }

    [Required]
    [JsonProperty("expiredAt")]
    public DateTime ExpiredAt { get; set; }

    public virtual UserContentRestrictionsType? UserRestrictionsType { get; set; }
}
