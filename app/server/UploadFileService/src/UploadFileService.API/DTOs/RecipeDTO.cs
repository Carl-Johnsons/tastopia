using Newtonsoft.Json;

namespace UploadFileService.API.DTOs;

public class RecipeDTO
{
    [JsonProperty("name")]
    public string? Name { get; set; } = null!;
}
