using Newtonsoft.Json;

namespace UploadFileService.API.DTOs;

public class TestDTO
{
    [JsonProperty("name")]
    public string name;
    //public List<string> ingredients;
    //public List <StepDTO> steps;
}

public class StepDTO
{
    public string name;
    public string index;
    public List<IFormFile> image;
}
