namespace EmailWorker.Interfaces;

public interface IEmailService
{
    Task SendEmail(string emailTo, string subject, string body, bool isHtml = false);
}