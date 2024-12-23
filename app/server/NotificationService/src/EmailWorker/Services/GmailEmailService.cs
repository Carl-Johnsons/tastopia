using EmailWorker.Interfaces;
using EmailWorker.Utilities;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MimeKit;
using System.Text;

namespace EmailWorker.Services;

// Ref: https://stackoverflow.com/questions/31209273/how-do-i-set-return-uri-for-googlewebauthorizationbroker-authorizeasync
public class GmailEmailService : IEmailService
{
    private readonly string[] Scopes = { GmailService.Scope.GmailSend };
    private readonly string APPLICATION_NAME;
    private readonly string EMAIL_ADDRESS;

    public GmailEmailService()
    {
        EnvUtility.LoadEnvFile();
        APPLICATION_NAME = DotNetEnv.Env.GetString("APPLICATION_NAME", "Not Found");
        EMAIL_ADDRESS = DotNetEnv.Env.GetString("EMAIL_ADDRESS", "Not found");
    }

    public async Task SendEmail(string emailTo, string subject, string body, bool isHtml = false)
    {
        var credential = await GetUserCredentialAsync();

        var service = new GmailService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = APPLICATION_NAME,
        });

        var emailMessage = CreateEmailMessage(emailTo, subject, body, isHtml);

        var request = service.Users.Messages.Send(emailMessage, "me");
        await request.ExecuteAsync();
    }

    private async Task<UserCredential> GetUserCredentialAsync()
    {
        using var stream = new FileStream(Directory.GetCurrentDirectory() + "/credentials.json", FileMode.Open, FileAccess.Read);
        string credPath = Directory.GetCurrentDirectory() + "/Tokens";

        // Create a custom data store to save the token in token.json file
        var fileDataStore = new FileDataStore(credPath, true);

        return await GoogleWebAuthorizationBroker.AuthorizeAsync(
            GoogleClientSecrets.FromStream(stream).Secrets,
            Scopes,
            "user",
            CancellationToken.None,
            new FileDataStore(credPath, true)
        );
    }

    private Message CreateEmailMessage(string to, string subject, string body, bool isHtml)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(APPLICATION_NAME, EMAIL_ADDRESS));
        mimeMessage.To.Add(new MailboxAddress("", to));
        mimeMessage.Subject = subject;
        mimeMessage.Body = new TextPart(isHtml ? "html" : "plain") { Text = body };

        return new Message
        {
            Raw = Base64UrlEncode(mimeMessage.ToString())
        };
    }

    private string Base64UrlEncode(string input)
    {
        byte[] byteArray = Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(byteArray)
                     .Replace('+', '-')
                     .Replace('/', '_')
                     .Replace("=", "");
    }
}
