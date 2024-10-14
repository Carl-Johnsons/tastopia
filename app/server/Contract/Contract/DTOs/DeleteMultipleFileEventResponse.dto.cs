using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs;

public class DeleteMultipleFileEventResponseDTO
{
    [Required]
    [JsonProperty("result")]
    public string Result { get; set; } = null!;
}