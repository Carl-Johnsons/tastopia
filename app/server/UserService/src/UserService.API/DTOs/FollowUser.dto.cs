using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UserService.API.DTOs;

public class FollowUserDTO
{
    [Required]
    [JsonProperty("accountId")]
    public Guid AccountId { get; set; }
}
