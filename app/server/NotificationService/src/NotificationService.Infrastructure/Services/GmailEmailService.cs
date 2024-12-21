using NotificationService.Infrastructure.Utilities;
using System.Net;
using System.Net.Mail;

namespace NotificationService.Infrastructure.Services;

public class GmailEmailService : IEmailService
{
    private readonly string SMTPServer = "smtp.gmail.com";
    private readonly int SMTPPort = 587;
    private string EmailAddress;
    private string EmailPassword;

    public GmailEmailService()
    {
        EnvUtility.LoadEnvFile();
        EmailAddress = DotNetEnv.Env.GetString("EMAIL_ADDRESS", "Not Found");
        EmailPassword = DotNetEnv.Env.GetString("EMAIL_PASSWORD", "Not Found");
    }

    public async Task SendEmail(string emailTo, string subject, string body, bool isHtml = false)
    {
        if (!IsValidEmail(emailTo))
            throw new ArgumentException("Invalid recipient email address.", nameof(emailTo));
        if (!IsValidEmail(EmailAddress))
            throw new InvalidOperationException("Configured email address is invalid.");

        using var smtpClient = new SmtpClient(SMTPServer, SMTPPort)
        {
            Credentials = new NetworkCredential(EmailAddress, EmailPassword),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(EmailAddress),
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml
        };

        mailMessage.To.Add(emailTo);

        await smtpClient.SendMailAsync(mailMessage);
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }


}
