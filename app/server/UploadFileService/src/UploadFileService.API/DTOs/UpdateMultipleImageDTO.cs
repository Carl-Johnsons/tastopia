using Newtonsoft.Json;

namespace UploadFileService.API.DTOs;

public class UpdateMultipleImageDTO
{
    [JsonProperty("files")]    
    public List<IFormFile>? Files { get; set; }

    [JsonProperty("urls")]
    public List<string>? Urls { get; set; }
}
