namespace SMSWorker.Interfaces;
public interface ISMSService
{
    Task SendSMS(string phoneTo, string message);
}