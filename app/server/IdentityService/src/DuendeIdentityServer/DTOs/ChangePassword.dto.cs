namespace DuendeIdentityServer.DTOs;

public class ChangePasswordDTO
{
    public string Identifier { get; set; } = null!;
    public string OTP { get; set; } = null!;
    public string Password { get; set; } = null!;
}
