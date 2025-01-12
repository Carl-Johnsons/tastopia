using Newtonsoft.Json;
namespace UploadFileService.API.DTOs;

public class UpdateMultipleImageFileDTO
{
    [JsonProperty("files")]
    public List<IFormFile>? Files { get; set; } = null!;

    [JsonProperty("deleteUrls")]
    public List<string>? DeleteUrls { get; set; } = null!;
}
