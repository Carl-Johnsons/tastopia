using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UploadFileService.API.DTOs;

public class DeleteMultipleImageFileDTO
{
    [Required]
    [JsonProperty("deleteUrls")]
    public List<string> DeleteUrls { get; set; } = null!;
}
