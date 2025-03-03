namespace DuendeIdentityServer.DTOs;

public class CheckForgotPasswordDTO
{
    public string Identifier { get; set; } = null!;
    public string OTP { get; set; } = null!;
}
