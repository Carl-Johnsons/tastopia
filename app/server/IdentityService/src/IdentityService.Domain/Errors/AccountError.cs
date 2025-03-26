using System.Net;

namespace IdentityService.Domain.Errors;

public class AccountError
{
    public static Error NotFound =>
        new("AccountError.NotFound",
            StatusCode: (int)HttpStatusCode.NotFound,
            Message: "Account not found!");
    public static Error EmailNotFound =>
        new("AccountError.EmailNotFound",
            StatusCode: (int)HttpStatusCode.NotFound,
            Message: "Account's email not found!");
    public static Error PhoneNotFound =>
        new("AccountError.PhoneNotFound",
            StatusCode: (int)HttpStatusCode.NotFound,
            Message: "Account's email not found!");
    public static Error EmailAlreadyExisted =>
        new("AccountError.EmailAlreadyExisted",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Email already existed!");
    public static Error PhoneAlreadyExisted =>
        new("AccountError.PhoneAlreadyExisted",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Phone already existed!");
    public static Error CreateAccountFailed =>
        new("AccountError.CreateAccountFailed",
            StatusCode: (int)HttpStatusCode.InternalServerError,
            Message: "There is something wrong when creating an account");
    public static Error InvalidOTP =>
        new("AccountError.InvalidOTP",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Account's OTP is invalid");
    public static Error OTPExpired =>
        new("AccountError.OTPExpired",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Account's OTP is expired");
    public static Error VerifyFailed =>
        new("AccountError.VerifyFailed",
            StatusCode: (int)HttpStatusCode.InternalServerError,
            Message: "Verify account unsuccessful");
    public static Error EmailAlreadyConfirmed =>
        new("AccountError.EmailAlreadyConfirmed",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Email already confirmed");
    public static Error EmailNotConfirmed =>
        new("AccountError.EmailNotConfirmed",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Email not confirmed");
    public static Error PhoneAlreadyConfirmed =>
        new("AccountError.PhoneAlreadyConfirmed",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Phone already confirmed");
    public static Error PhoneNotConfirmed =>
        new("AccountError.PhoneNotConfirmed",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Phone not confirmed");
    public static Error ResendOTPFailed =>
        new("AccountError.ResendOTPFailed",
            StatusCode: (int)HttpStatusCode.InternalServerError,
            Message: "Resend OTP Failed");
    public static Error LinkAccountFailed =>
        new("AccountError.LinkAccountFailed",
            StatusCode: (int)HttpStatusCode.InternalServerError,
            Message: "Link Account Failed");

    public static Error UnlinkAccountFailed =>
        new("AccountError.UnlinkAccountFailed",
            StatusCode: (int)HttpStatusCode.InternalServerError,
            Message: "Unlink Account Failed");
    public static Error UsernameAlreadyExisted =>
        new("AccountError.UsernameAlreadyExisted",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Username already existed");
    public static Error UpdateAccountFailed =>
        new("AccountError.UpdateAccountFailed",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Operation update account failed");
    public static Error InvalidAccountMethod =>
        new("AccountError.InvalidAccountMethod",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Invalid account method");
    public static Error ResetPasswordFailed =>
        new("AccountError.ResetPasswordFailed",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Reset Password failed");
    public static Error NullParameter =>
        new("AccountError.NullParameter",
            StatusCode: (int)HttpStatusCode.InternalServerError,
            Message: "Null Parameter");
}