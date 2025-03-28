using Contract.Utilities;
using SMSWorker.Interfaces;
using System.Text;
namespace SMSWorker.Services;
public class SMSService : ISMSService
{
    private readonly string SPEEDSMS_DEVICE_ID;
    private readonly string SPEEDSMS_API_URL;
    private readonly string SPEEDSMS_API_TOKEN;

    public SMSService()
    {
        EnvUtility.LoadEnvFile();
        SPEEDSMS_DEVICE_ID = DotNetEnv.Env.GetString("SPEEDSMS_DEVICE_ID", "Not Found");
        SPEEDSMS_API_URL = DotNetEnv.Env.GetString("SPEEDSMS_API_URL", "Not found");
        SPEEDSMS_API_TOKEN = DotNetEnv.Env.GetString("SPEEDSMS_API_TOKEN", "Not found");
    }

    public async Task SendSMS(string phoneTo, string message)
    {
        string url = $"{SPEEDSMS_API_URL}?to={phoneTo}&content={Uri.EscapeDataString(message)}&sender={SPEEDSMS_DEVICE_ID}";
        string authHeader = $"Basic {SPEEDSMS_API_TOKEN}";
        await Console.Out.WriteLineAsync("Token:"+ SPEEDSMS_API_TOKEN);
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", authHeader);

            HttpContent content = new StringContent("{}", Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);
            string responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseContent);
        }
    }
}
