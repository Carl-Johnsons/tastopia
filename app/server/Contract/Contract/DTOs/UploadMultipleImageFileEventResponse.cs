using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs;

public class UploadMultipleImageFileEventResponseDTO
{
    [Required]
    [JsonProperty("files")]
    public List<UploadImageFileEventResponseDTO> Files { get; set; } = null!;
}