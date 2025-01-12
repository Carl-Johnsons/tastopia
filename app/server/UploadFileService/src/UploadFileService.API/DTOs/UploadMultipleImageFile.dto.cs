using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UploadFileService.API.DTOs;

public class UploadMultipleImageFileDTO
{
    [Required]
    [JsonProperty("files")]
    public List<IFormFile> Files { get; set; } = null!;
}
