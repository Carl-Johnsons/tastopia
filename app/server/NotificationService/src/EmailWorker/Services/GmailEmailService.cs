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
/*
 * When you deploy this to docker you will get an exception that the operating system does not support running the process. Basically, its trying to open a browser on the Metal Box. which is not possible with docker.
   To solve this. Modify the code to use full absolute path like this:
 * 
 * ```
 * var inputFolderAbsolute = Path.Combine(AppContext.BaseDirectory, "Auth.Store");
    ...
    new FileDataStore(inputFolderAbsolute, true)
 * ```
 * Run this application as a console app on your local machine so the browser opens.
   - Select the account you want to work with
   - In the bin folder, a new folder and file will be created.
   - Copy that folder to the root path
   - Set the file to copy if newer
   - Deploy to docker
   - Because the refresh token is saved for the account you selected it will get a new access token and work.
   - NB: It is possible the refresh token expires to whatever reason. You will have to repeat the steps above
 */


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
        var inputFolderAbsolute = Path.Combine(AppContext.BaseDirectory, "Auth.Store");

        // Create a custom data store to save the token in token.json file
        var fileDataStore = new FileDataStore(inputFolderAbsolute, true);

        return await GoogleWebAuthorizationBroker.AuthorizeAsync(
            GoogleClientSecrets.FromStream(stream).Secrets,
            Scopes,
            "user",
            CancellationToken.None,
            fileDataStore
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
