using Twilio.Rest.Api.V2010.Account;

namespace NotificationService.Infrastructure.Services;

public class TwilioSMSService : ISMSService
{
    private string ACCOUNT_SID = "your_account_sid";
    private string AUTH_TOKEN = "your_auth_token";

    public void SendTextMessage(string phoneNumber, string message)
    {
        // Send SMS
        var m = MessageResource.Create(
            body: message,
            from: new Twilio.Types.PhoneNumber("your_twilio_phone_number"),
            to: new Twilio.Types.PhoneNumber("recipient_phone_number")
        );
        // 0364814932
        Console.WriteLine($"Message sent! SID: {m.Sid}");

    }
}
