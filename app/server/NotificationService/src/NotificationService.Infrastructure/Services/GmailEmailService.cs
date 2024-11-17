using System.Net;
using System.Net.Mail;

namespace NotificationService.Infrastructure.Services;

public class GmailEmailService
{
    private readonly string SMTPServer = "smtp.gmail.com";
    private readonly int SMTPPort = 587;
    private string EmailAddress;
    private string EmailPassword;

    public GmailEmailService(string emailAddress, string emailPassword)
    {
        EmailAddress = emailAddress;
        EmailPassword = emailPassword;
    }

    public async Task SendEmail(string emailTo, string subject, string body, bool isHtml = false)
    {
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
}
