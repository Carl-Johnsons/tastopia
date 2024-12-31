using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UploadFileService.Domain.DTOs;

public class UploadMultipleFileDTO
{
    [Required]
    [JsonProperty("files")]
    public List<IFormFile> Files { get; set; } = null!;
}
