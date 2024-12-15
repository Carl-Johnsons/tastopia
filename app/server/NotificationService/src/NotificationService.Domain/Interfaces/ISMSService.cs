namespace NotificationService.Domain.Interfaces;

public interface ISMSService
{
    void SendTextMessage(string phoneNumber, string message);
}
