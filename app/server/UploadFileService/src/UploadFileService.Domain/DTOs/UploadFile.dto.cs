using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace UploadFileService.Domain.DTOs;

public class UploadFileDTO
{
    [Required]
    [JsonProperty("file")]
    public IFormFile File { get; set; } = null!;
}
