using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace UserService.API.DTOs;
public class CreateUserSearchUserDTO
{
    [Required]
    [JsonProperty("keyword")]
    public string Keyword { get; set; } = null!;
}
