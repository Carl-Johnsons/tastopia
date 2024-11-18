
namespace NotificationService.Infrastructure.Services
{
    public interface IEmailService
    {
        Task SendEmail(string emailTo, string subject, string body, bool isHtml = false);
    }
}