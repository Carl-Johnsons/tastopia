using System.Security.Cryptography;

namespace IdentityService.Infrastructure.Utilities;

public class OTPUtility
{
    private static int BASE_LENGTH = 6;

    /// <summary>
    /// Generates a 6 length numeric OTP.
    /// </summary>
    /// <param name="length">The length of the OTP to generate.</param>
    /// <returns>A numeric OTP as a string.</returns>
    public static string GenerateNumericOTP()
    {
        return GenerateNumericOTP(BASE_LENGTH);
    }

    /// <summary>
    /// Generates a numeric OTP of the specified length.
    /// </summary>
    /// <param name="length">The length of the OTP to generate.</param>
    /// <returns>A numeric OTP as a string.</returns>
    public static string GenerateNumericOTP(int length)
    {
        if (length <= 0)
            throw new ArgumentException("Length must be greater than zero.", nameof(length));

        var otp = new char[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            var data = new byte[length];
            rng.GetBytes(data);

            for (int i = 0; i < length; i++)
            {
                // Restrict each digit to 0-9
                otp[i] = (char)('0' + data[i] % 10);
            }
        }

        return new string(otp);
    }

    /// <summary>
    /// Generates an 6 length alphanumeric OTP.
    /// </summary>
    /// <param name="length">The length of the OTP to generate.</param>
    /// <returns>An alphanumeric OTP as a string.</returns>
    public static string GenerateAlphanumericOTP()
    {
        return GenerateAlphanumericOTP(BASE_LENGTH);
    }

    /// <summary>
    /// Generates an alphanumeric OTP of the specified length.
    /// </summary>
    /// <param name="length">The length of the OTP to generate.</param>
    /// <returns>An alphanumeric OTP as a string.</returns>
    public static string GenerateAlphanumericOTP(int length)
    {
        if (length <= 0)
            throw new ArgumentException("Length must be greater than zero.", nameof(length));

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var otp = new char[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            var data = new byte[length];
            rng.GetBytes(data);

            for (int i = 0; i < length; i++)
            {
                otp[i] = chars[data[i] % chars.Length];
            }
        }

        return new string(otp);
    }
}
