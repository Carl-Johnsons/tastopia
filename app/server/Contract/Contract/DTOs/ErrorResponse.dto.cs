using Newtonsoft.Json;

namespace Contract.DTOs;

public class ErrorResponseDTO
{
    [JsonProperty("status")]
    public int Status { get; set; }
    [JsonProperty("code")]
    public string Code { get; set; } = null!;
    [JsonProperty("message")]
    public string Message { get; set; } = null!;
}
