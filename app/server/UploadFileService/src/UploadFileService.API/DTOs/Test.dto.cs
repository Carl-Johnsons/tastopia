using Newtonsoft.Json;

namespace UploadFileService.API.DTOs;

public class TestDTO
{
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("ingredients")]
    public List<string> Ingredients { get; set; } = null!;

    [JsonProperty("steps")]
    public List<StepDTO> Steps { get; set; } = null!;
}

public class StepDTO
{
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("index")]
    public string Index { get; set; } = null!;

    [JsonProperty("image")]
    public List<IFormFile> Image { get; set; } = null!; 
}
