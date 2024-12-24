
using System.Net;

namespace IdentityService.Domain.Errors;

public class AccountError
{
    public static Error NotFound =>
        new("AccountError.NotFound",
            StatusCode: (int)HttpStatusCode.NotFound,
            Message: "Account not found!");
    public static Error EmailAlreadyExisted =>
        new("AccountError.NotFound",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Email already existed!");
    public static Error PhoneAlreadyExisted =>
        new("AccountError.NotFound",
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
    public static Error PhoneAlreadyConfirmed =>
        new("AccountError.PhoneAlreadyConfirmed",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Phone already confirmed");
}
