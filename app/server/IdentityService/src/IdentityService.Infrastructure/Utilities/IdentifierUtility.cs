using Contract.Constants;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace IdentityService.Infrastructure.Utilities;

public static class IdentifierUtility
{
    private static readonly string phoneNumberRegexPattern = @"^(?:(?:\+|00)84|0)(3[2-9]|5[2|5-9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$";

    public static AccountMethod Check(string identifier)
    {
        if (Regex.IsMatch(identifier, phoneNumberRegexPattern))
        {
            return AccountMethod.Phone;
        }
        try
        {
            var email = new MailAddress(identifier);
            return AccountMethod.Email;
        }
        catch (Exception)
        {
            return AccountMethod.Username;
        }
    }
}
