using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using RecipeWorker.Interfaces;

namespace RecipeWorker.Services;

public class OffensiveTextCheckerService : IOffensiveTextCheckerService
{
    private string apiKey {  get; init; }
    private string endpoint {  get; init; }
    private readonly ILogger<OffensiveTextCheckerService> _logger;

    public OffensiveTextCheckerService(ILogger<OffensiveTextCheckerService> logger)
    {
        apiKey = DotNetEnv.Env.GetString("DEEPINFRA_API_KEY", "Not found");
        endpoint = DotNetEnv.Env.GetString("DEEPINFRA_ENDPOINT", "Not found");
        _logger = logger;
    }

    public async Task<string> CheckOffensiveText(string text)
    {
        try {

            if(apiKey == "Not found" || endpoint == "Not found")
            {
                _logger.LogError("Not found api key or endpoint.");
                return "";
            }
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    input = $"<bos><start_of_turn>user\nHãy đánh giá văn bản sau và cho biết phần trăm khả năng câu này có chứa ngôn từ thô tục hoặc xúc phạm. Văn bản có thể không có dấu. Chỉ trả về phần trăm.\n{text}<end_of_turn>\n<start_of_turn>model\n",
                    stop = new[] { "<eos>", "<end_of_turn>" }
                };

                string jsonPayload = JsonConvert.SerializeObject(requestBody);

                HttpResponseMessage response = await client.PostAsync(endpoint, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(responseContent);
                    string? generatedText = jsonObject["results"]?[0]?["generated_text"]?.ToString();
                    return generatedText!;
                }
                _logger.LogError(JsonConvert.SerializeObject("Error api", Formatting.Indented));
                return "";
            }
        } catch (Exception ex) {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return "";
        }

    }
}
