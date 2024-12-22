
namespace IdentityService.Domain.Errors;

public class AccountError
{
    public static Error CreateAccountFailed => 
        new("AccountError.CreateAccountFailed",
            "There is something wrong when creating an account");
}
