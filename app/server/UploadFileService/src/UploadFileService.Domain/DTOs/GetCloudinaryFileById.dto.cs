using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UploadFileService.Domain.DTOs;

public class GetCloudinaryFileByIdDTO
{
    [Required]
    [JsonProperty("id")]
    public Guid? Id { get; set; }
}
