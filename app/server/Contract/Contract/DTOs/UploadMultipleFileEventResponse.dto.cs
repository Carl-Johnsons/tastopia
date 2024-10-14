using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs;

public class UploadMultipleFileEventResponseDTO
{
    [Required]
    [JsonProperty("files")]
    public List<UploadFileEventResponseDTO> Files { get; set; } = null!;
}